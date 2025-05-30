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
            modelBuilder.Entity<Genre>().Property(g => g.Name)
                .HasMaxLength(25).IsRequired();

            //================================================
            //Table: Actors
            //================================================
            modelBuilder.Entity<Actor>().HasKey(a=>a.Id);
            modelBuilder.Entity<Actor>().Property(a => a.Name)
                .HasMaxLength(25).IsRequired();
            modelBuilder.Entity<Actor>().Property(a=>a.Biography)
                .IsRequired();
            modelBuilder.Entity<Actor>().Property(a => a.DateOfBirth)
                .HasColumnType("date");

            //================================================
            //Table: Cine
            //================================================
            modelBuilder.Entity<Cine>().HasKey(c => c.Id);
            modelBuilder.Entity<Cine>().Property(c => c.Name)
                .HasMaxLength(150)
                .IsRequired();
            //modelBuilder.Entity<Cine>().Property(c => c.Price).HasPrecision(precision: 9, scale: 2);

            //================================================
            //Table: Movie
            //================================================
            modelBuilder.Entity<Movie>().HasKey(m => m.Id);
            modelBuilder.Entity<Movie>().Property(m=>m.Title)
                .HasMaxLength(250)
                .IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.PosterUrl)
                .HasMaxLength(500).IsUnicode(false)
                .IsRequired();
            modelBuilder.Entity<Movie>().Property(m=>m.ReleaseDate)
                .HasColumnType("date");

            //================================================
            //Table: Movie
            //================================================
            modelBuilder.Entity<CineOffer>().HasKey(co => co.Id);
            modelBuilder.Entity<CineOffer>().Property(co => co.StartDate)
                .HasColumnType("date").IsRequired();
            modelBuilder.Entity<CineOffer>().Property(co => co.EndDate)
                .HasColumnType("date").IsRequired();
            modelBuilder.Entity<CineOffer>().Property(co => co.DiscountPercentage)
                .HasPrecision(precision: 5, scale: 2).IsRequired();

            //================================================
            //Table: Cine Room
            //================================================
            modelBuilder.Entity<CineRoom>().HasKey(cr => cr.Id);
            modelBuilder.Entity<CineRoom>().Property(cr => cr.Price)
                .HasPrecision(precision: 9, scale: 2)
                .IsRequired();
            modelBuilder.Entity<CineRoom>().Property(cr => cr.CineRoomType)
                .HasDefaultValue(CineRoomType.CRT_2D);

            //================================================
            //Table: Movies and Actors
            //================================================
            //key compuesta: MovieId, ActorId
            modelBuilder.Entity<MovieActor>().HasKey(ma => new { ma.MovieId, ma.ActorId });
            modelBuilder.Entity<MovieActor>().Property(ma => ma.Character)
                .HasMaxLength(150);
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cine> Cines { get; set; }
        public DbSet<CineOffer> CineOffers { get; set; }
        public DbSet<CineRoom> CineRooms { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MoviesActors { get; set; }
    }
}
