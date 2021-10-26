using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTO_s;
using PeliculasApi.Entidades;
using PeliculasApi.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : ControllerBase
    {
        private readonly AplicationDBContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly String contenedor = "posters";
        public PeliculasController(AplicationDBContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<PeliculaDTO>>> Get()
        {
            var peliculas = await context.Peliculas.ToListAsync();
            return mapper.Map<List<PeliculaDTO>>(peliculas);
        }
        [HttpGet("{id:int}", Name = "ObtenerPelicula")]
        public async Task<ActionResult<PeliculaDTO>> Get(int id)
        {
            var pelicula = await context.Peliculas.FirstOrDefaultAsync(peiculaDB => peiculaDB.id == id);
            if (pelicula == null)
            {
                return NotFound();
            }
            return mapper.Map<PeliculaDTO>(pelicula);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] peliculaCreacionDTO peliculaDto)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaDto);

            if (peliculaDto.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaDto.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaDto.Poster.FileName);
                    pelicula.Poster = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, peliculaDto.Poster.ContentType);
                }
            }
            context.Add(pelicula);
            await context.SaveChangesAsync();
            var VerPeliculaDTO = mapper.Map<PeliculaDTO>(pelicula);
            return new CreatedAtRouteResult("ObtenerPelicula", new { id = pelicula.id }, VerPeliculaDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromForm] peliculaCreacionDTO peliculaDto, [FromRoute] int id)
        {
            var Pelicula = await context.Peliculas.FirstOrDefaultAsync(x => x.id == id);
            if (Pelicula == null) { return NotFound(); }

            Pelicula = mapper.Map(peliculaDto, Pelicula);

            if (peliculaDto.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaDto.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaDto.Poster.FileName);
                    Pelicula.Poster = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor, Pelicula.Poster, peliculaDto.Poster.ContentType);
                }
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete([FromRoute] int id)
        {
            var existe = await context.Peliculas.AnyAsync(x => x.id == id);
            if (!existe) { return NotFound("La peicula con id " + id + " No existe"); }

            context.Remove(new Pelicula() { id = id });
            await context.SaveChangesAsync();
            return NoContent();

        }
    }
}