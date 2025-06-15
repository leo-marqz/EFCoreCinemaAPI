using EFCoreCinemaAPI.Models;
using EFCoreCinemaAPI.Models.Keyless;
using EFCoreCinemaAPI.Models.Seeding;
using EFCoreCinemaAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EFCoreCinemaAPI
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IUserService userService;

        //public ApplicationDbContext()
        //{
        //    // Constructor vacio necesario para usar OnConfiguring
        //    // si no se configura en el Program.cs o Startup.cs
        //}

        public ApplicationDbContext(DbContextOptions options, IUserService userService) 
            : base(options)
        {
            this.userService = userService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Procesar las entidades antes de guardar - se modifico en SaveChangesAsync
            ProcessSaving();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void ProcessSaving()
        {
            foreach(var item in ChangeTracker.Entries().Where(e=>e.State == EntityState.Added 
            && e.Entity is AuditableEntity))
            {
                var entity = item.Entity as AuditableEntity;
                entity.CreatedBy = userService.GetUserById(); // Aquí podrías obtener el usuario actual si tienes un contexto de usuario
                entity.ModifiedBy = userService.GetUserById();
            }

            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified
            && e.Entity is AuditableEntity))
            {
                var entity = item.Entity as AuditableEntity;
                entity.ModifiedBy = userService.GetUserById();

                // No modificar CreatedBy al actualizar
                item.Property(nameof(entity.CreatedBy)).IsModified = false; 
            }
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    //Esto lo usamos si no queremos configurarlo en el Program.cs o Startup.cs
        //    //Para ello tambien debemos agregar un constructor vacio
        //    //luego, llamar el dbcontext en el Program.cs o Startup.cs
        //    // builder.Services.AddDbContext<ApplicationDbContext>();
        //    base.OnConfiguring(options);
        //    if (!options.IsConfigured)
        //    {
        //        options.UseSqlServer("name=DefaultConnection", opt =>
        //        {
        //            opt.UseNetTopologySuite(); // Enable NetTopologySuite for spatial data types
        //        }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); // Set default tracking behavior to NoTracking
        //    }
        //}

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

            //modelBuilder.Ignore<Address>(); // Ignoring Address 

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

            //Tabla por Tipo
            modelBuilder.Entity<Merchandising>().ToTable("Merchandising");
            modelBuilder.Entity<Laptop>().ToTable("Laptops");

            var laptopHp = new Laptop
            {
                Id = 1,
                Brand = "HP",
                Model = "Pavilion 15",
                Price = 799.99m,
                Processor = "Intel Core i7",
                RAM = 16,
                Storage = 512
            };

            var merchandisingItem = new Merchandising
            {
                Id = 2,
                Name = "Cine T-Shirt",
                Price = 19.99m,
                IsAvailable = true,
                IsClothes = false,
                IsCollectible = true,
                Volume = 0.5, // Assuming volume is in liters
                Weight = 0.2 // Assuming weight is in kilograms
            };

            modelBuilder.Entity<Laptop>().HasData(laptopHp);
            modelBuilder.Entity<Merchandising>().HasData(merchandisingItem);

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

        public DbSet<CineProfile> CineProfiles { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Product> Products { get; set; }

    }
}
