using EFCoreCinemaAPI.Models;
using EFCoreCinemaAPI.Models.Keyless;
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
            SeedingUsersMessages.Seed(modelBuilder);

            //modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //modelBuilder.Entity<Log>().Property(lg => lg.Id).ValueGeneratedNever();

            modelBuilder.Ignore<Address>(); // Ignoring Address 

            modelBuilder.Entity<CineWithoutLocation>()
                .ToSqlQuery("SELECT Id, Name FROM Cines") // Using a raw SQL query to define the entity
                .HasNoKey() // Configuring a keyless entity
                .ToView(null); // This will not create a table, but allows querying as if it were a table

            // Configuring Metrics as a keyless view
            modelBuilder.Entity<Metric>().HasNoKey().ToView("Metrics"); 

            //esto es algo que no es cubierno por las convenciones de EF Core
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Configuring all string properties to use UTF-8 encoding
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(string) && property.Name.Contains("URL", StringComparison.CurrentCultureIgnoreCase))
                    {
                        property.SetIsUnicode(false); // Set to true for UTF-8 encoding
                        property.SetMaxLength(500); // Set a maximum length for URL fields
                    }
                }
            }

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cine> Cines { get; set; }
        public DbSet<CineOffer> CineOffers { get; set; }
        public DbSet<CineRoom> CineRooms { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MoviesActors { get; set; }

        public DbSet<Log> Logs { get; set; }

        //Esto seria para evitar el usar _context.Set<CineWithoutLocation>() en los controladores
        public DbSet<CineWithoutLocation> CinesWithoutLocations { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
