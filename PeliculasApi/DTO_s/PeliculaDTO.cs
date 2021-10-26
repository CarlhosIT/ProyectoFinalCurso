using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTO_s
{
    public class PeliculaDTO
    {
        public int id { get; set; }

        public String Titulo { get; set; }
        public bool enCine { get; set; }
        public DateTime FechaEstreno { get; set; }
        public String Poster { get; set; }
    }
}
