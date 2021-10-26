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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PeliculasActor>().HasKey(x=> new { x.PelicuaId,x.ActorId});
            modelBuilder.Entity<PeliculasGenero>().HasKey(x => new { x.PeliculaId, x.GeneroId });

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; } 
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<PeliculasActor> PeliculasActor { get; set; }
        public DbSet<PeliculasGenero> PeliculasGenero { get; set; }
    }
}
