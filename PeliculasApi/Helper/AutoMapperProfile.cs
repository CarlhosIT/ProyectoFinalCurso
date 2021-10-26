using AutoMapper;
using PeliculasApi.DTO_s;
using PeliculasApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Genero,GeneroDTOView>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<Actor, ActorDTOView>().ReverseMap();
            CreateMap<ActorDTOCreacion, Actor>().ForMember(x => x.Imagen, option => option.Ignore());
            CreateMap<ActorPatchDTO, Actor>().ReverseMap();

            CreateMap<peliculaCreacionDTO, Pelicula>().ForMember(x => x.Poster, option => option.Ignore())
                .ForMember(X=>X.PeliculasGeneros,option=>option.MapFrom(MappPeliculaGenero)).
                ForMember(X => X.PeliculasActores, option => option.MapFrom(MappPeliculaActor));
            CreateMap<Pelicula, PeliculaDTO>().ReverseMap();
        }

        private List<PeliculasGenero> MappPeliculaGenero(peliculaCreacionDTO peliculaCreacionDTO,Pelicula pelicula) 
        {
            var resultado = new List< PeliculasGenero >();

            if (peliculaCreacionDTO.GenerosId == null) { return resultado; }

            foreach (var id in peliculaCreacionDTO.GenerosId)
            {
                resultado.Add(new PeliculasGenero(){ GeneroId=id});
            }
            return resultado;
        }

        private List<PeliculasActor> MappPeliculaActor(peliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula) 
        {
            var resultado = new List<PeliculasActor>();

            if (peliculaCreacionDTO.Actores == null) { return resultado; }

            foreach (var actor in peliculaCreacionDTO.Actores) 
            {
                resultado.Add(new PeliculasActor() { ActorId=actor.ActorId,Personaje=actor.Personaje});
            }
            return resultado;
        }

    }
}
