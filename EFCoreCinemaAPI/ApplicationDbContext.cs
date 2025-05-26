using EFCoreCinemaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCinemaAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //================================================
            //Table: Genres
            //================================================
            modelBuilder.Entity<Genre>().HasKey(g => g.Id);
            modelBuilder.Entity<Genre>().Property(g => g.Name).HasMaxLength(25).IsRequired();

            //================================================
            //Table: Actors
            //================================================
            modelBuilder.Entity<Actor>().HasKey(a=>a.Id);
            modelBuilder.Entity<Actor>().Property(a => a.Name)
                .HasMaxLength(25).IsRequired();
            modelBuilder.Entity<Actor>().Property(a=>a.Biography).IsRequired();
            modelBuilder.Entity<Actor>().Property(a => a.DateOfBirth).HasColumnType("date");

            //================================================
            //Table: Cine
            //================================================
            modelBuilder.Entity<Cine>().HasKey(c => c.Id);
            modelBuilder.Entity<Cine>().Property(c => c.Name).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Cine>().Property(c => c.Price).HasPrecision(precision: 9, scale: 2); ;
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cine> Cines { get; set; }
    }
}
