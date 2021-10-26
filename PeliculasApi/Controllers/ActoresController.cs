using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTO_s;
using PeliculasApi.Entidades;
using PeliculasApi.Helper;
using PeliculasApi.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly AplicationDBContext context;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly String contenedor = "actores";

        public ActoresController(IMapper mapper, AplicationDBContext context, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.mapper = mapper;
            this.context = context;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTOView>>> Get([FromQuery] PaginaDTO paginacionDTO)
        {
            var queryable =  context.Actores.AsQueryable();
            await HttpContext.InsertarParametros(queryable,paginacionDTO.RegistroPorPagina);
            var entidades = await queryable.Paginacion(paginacionDTO).ToListAsync();
            var dtos = mapper.Map<List<ActorDTOView>>(entidades);
            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDTOView>> Get([FromRoute] int id)
        {
            var entidad = await context.Actores.FirstOrDefaultAsync(actorBD => actorBD.Id == id);
            if (entidad == null) { return NotFound("El actor no ha sido encontrado"); }

            var dtos = mapper.Map<ActorDTOView>(entidad);
            return dtos;

        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchDTO> patchDocument)
        {
            if (patchDocument == null) { return BadRequest(); }
            
            var actor = await context.Actores.FirstOrDefaultAsync(actorBD => actorBD.Id == id);
            if (actor == null) { return NotFound(); }
            
            var EntidadDTO = mapper.Map<ActorPatchDTO>(actor);
            patchDocument.ApplyTo(EntidadDTO,ModelState);

            var esValido = TryValidateModel(EntidadDTO);
            if (!esValido) 
            {
                return BadRequest();
            }

            mapper.Map(EntidadDTO,actor);
            await context.SaveChangesAsync();
            return NoContent();

        }


        [HttpPost()]
        public async Task<ActionResult> Post([FromForm] ActorDTOCreacion actorDTO)
        {
            var entidad = mapper.Map<Actor>(actorDTO);

            if (actorDTO.Imagen!=null) 
            {
                using (var memoryStream = new MemoryStream()) 
                {
                    await actorDTO.Imagen.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorDTO.Imagen.FileName);
                    entidad.Imagen = await almacenadorArchivos.GuardarArchivo(contenido,extension,contenedor,actorDTO.Imagen.ContentType);
                }
            }

            context.Add(entidad);
            await context.SaveChangesAsync();
            var retornoGeneroDTO = mapper.Map<ActorDTOView>(entidad);
            return new CreatedAtRouteResult("obtenerActor", new { id = retornoGeneroDTO.Id }, retornoGeneroDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Post([FromForm] ActorDTOCreacion actorDTO, [FromRoute] int id)
        {
            var actorDB = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (actorDB == null) { return NotFound(); }

            actorDB=mapper.Map(actorDTO, actorDB);

            if (actorDTO.Imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorDTO.Imagen.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorDTO.Imagen.FileName);
                    actorDB.Imagen = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor,actorDB.Imagen, actorDTO.Imagen.ContentType);
                }
            }

            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete([FromRoute] int id)
        {
            var existe = await context.Actores.AnyAsync(generoBD => generoBD.Id == id);
            if (!existe) { return NotFound("EL actor id " + id + " No existe"); }

            context.Remove(new Actor() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
    
}
