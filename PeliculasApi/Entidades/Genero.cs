using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Entidades
{
    public class Genero
    {
        public int Id { get; set; }
        [Required]
        public String Nombre { get; set; }
        public List<PeliculasGenero> PeliculasGeneros { get; set; }
    }
}
