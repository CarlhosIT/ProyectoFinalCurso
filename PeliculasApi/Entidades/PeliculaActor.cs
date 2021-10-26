using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Entidades
{
    public class PeliculasActor
    {
        public int PelicuaId { get; set; }
        public int ActorId { get; set; }
        public String Personaje { get; set; }
        public int Orden { get; set; }
        public Pelicula pelicula { get; set; }
        public Actor actor { get; set; }
    }
}
