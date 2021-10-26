using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PeliculasApi.Helper;
using PeliculasApi.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTO_s
{
    public class peliculaCreacionDTO
    {
        public int id { get; set; }
        [Required]
        [StringLength(300)]
        public String Titulo { get; set; }
        public bool enCine { get; set; }
        public DateTime FechaEstreno { get; set; }
        [PesoIArchivoValidacion(4)]
        public IFormFile Poster { get; set; }



        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenerosId { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorePeliculaCreacion>>))]
        public List<ActorePeliculaCreacion> Actores { get; set; }



    }
}
