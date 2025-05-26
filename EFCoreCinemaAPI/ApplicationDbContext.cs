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

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
    }
}
