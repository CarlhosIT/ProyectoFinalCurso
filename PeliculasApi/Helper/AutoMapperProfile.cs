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
            CreateMap<ActorDTOCreacion, Actor>();
        }
    }
}
