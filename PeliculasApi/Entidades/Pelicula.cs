using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Entidades
{
    public class Pelicula
    {
        public int id { get; set; }
        [Required]
        [StringLength(300)]
        public String Titulo { get; set; }
        public bool enCine { get; set; }
        public DateTime FechaEstreno { get; set; }
        public String Poster { get; set; }

        public List<PeliculasGenero> PeliculasGeneros { get; set; }
        public List<PeliculasActor> PeliculasActores { get; set; }

    }
}
