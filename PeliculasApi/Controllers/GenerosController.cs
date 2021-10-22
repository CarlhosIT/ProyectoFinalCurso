using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTO_s;
using PeliculasApi.Entidades;
using PeliculasApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        private readonly AplicationDBContext context;
        public readonly IMapper mapper;

        public GenerosController(AplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<GeneroDTOView>>> Get()
        {
            var entidades = await context.Generos.ToListAsync();
            var dtos = mapper.Map<List<GeneroDTOView>>(entidades);
            return dtos;

        }
        [HttpGet("{id:int}", Name = "obtenerGeneros")]
        public async Task<ActionResult<GeneroDTOView>> Get([FromRoute] int id)
        {
            var entidad = await context.Generos.FirstOrDefaultAsync(genero => genero.Id == id);
            if (entidad == null) { return NotFound("El genero no ha sido encontrado"); }

            var dtos = mapper.Map<GeneroDTOView>(entidad);
            return dtos;

        }
        [HttpPost()]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoDTO)
        {
            var entidad = mapper.Map<Genero>(generoDTO);
            context.Add(entidad);
            await context.SaveChangesAsync();
            var retornoGeneroDTO = mapper.Map<GeneroDTOView>(entidad);
            return new CreatedAtRouteResult("obtenerGeneros", new { id = retornoGeneroDTO.Id }, retornoGeneroDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoDTO, [FromRoute] int id)
        {
            var entidad = mapper.Map<Genero>(generoDTO);
            if (entidad == null) { return NotFound("EL genero id " + id + " No existe"); }
            entidad.Id = id;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete([FromRoute] int id)
        {
            var existe = await context.Generos.AnyAsync(generoBD => generoBD.Id == id);
            if (!existe) { return NotFound("EL genero id " + id + " No existe"); }

            context.Remove(new Genero() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
