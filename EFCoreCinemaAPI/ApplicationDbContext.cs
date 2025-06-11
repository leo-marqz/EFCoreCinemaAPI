using EFCoreCinemaAPI.Models;
using EFCoreCinemaAPI.Models.Seeding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace EFCoreCinemaAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //Convenciones Globales
            configurationBuilder.Properties<decimal>().HavePrecision(9, 2);
            configurationBuilder.Properties<DateTime>().HaveColumnType("date"); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding initial data (Create Migration)
            SeedingQueryModule.Seed(modelBuilder);

            //modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //modelBuilder.Entity<Log>().Property(lg => lg.Id).ValueGeneratedNever();
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cine> Cines { get; set; }
        public DbSet<CineOffer> CineOffers { get; set; }
        public DbSet<CineRoom> CineRooms { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MoviesActors { get; set; }

        public DbSet<Log> Logs { get; set; }
    }
}
