using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTO_s;
using PeliculasApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Controllers
{
    [ApiController]
    [Route("api/cactores")]
    public class ActoresController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly AplicationDBContext context;

        public ActoresController(IMapper mapper, AplicationDBContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTOView>>> Get()
        {

            var entidades = await context.Actores.ToListAsync();
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

        [HttpPost()]
        public async Task<ActionResult> Post([FromForm] ActorDTOCreacion actorDTO)
        {
            var entidad = mapper.Map<Actor>(actorDTO);
            context.Add(entidad);
            await context.SaveChangesAsync();
            var retornoGeneroDTO = mapper.Map<ActorDTOView>(entidad);
            return new CreatedAtRouteResult("obtenerActor", new { id = retornoGeneroDTO.Id }, retornoGeneroDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Post([FromBody] ActorDTOCreacion actorDTO, [FromRoute] int id)
        {
            var entidad = mapper.Map<Actor>(actorDTO);
            if (entidad == null) { return NotFound("EL actor id " + id + " No existe"); }
            entidad.Id = id;
            context.Entry(entidad).State = EntityState.Modified;
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
