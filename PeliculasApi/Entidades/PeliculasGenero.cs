using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Entidades
{
    public class PeliculasGenero
    {
        public int GeneroId { get; set; }
        public int PeliculaId { get; set; }
        public Genero genero { get; set; }
        public Pelicula pelicula { get; set; }
    }
}
