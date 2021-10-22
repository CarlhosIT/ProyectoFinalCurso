using Microsoft.EntityFrameworkCore;
using PeliculasApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext( DbContextOptions options) : base(options)
        {
        }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
    }
}
