
using EFCoreCinemaAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EFCoreCinemaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>((options) =>
            {
                options.UseSqlServer(
                        builder.Configuration.GetConnectionString("DefaultConnection"),
                        (sqlServer) => sqlServer.UseNetTopologySuite()
                );
                //para mejorar el performance de solo lectura
                //para seguimiento de actualizaciones se debe agregar el AsTracking a cada consulta
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                //options.UseLazyLoadingProxies(); // Habilitar Lazy Loading
            });

            builder.Services.AddAutoMapper(typeof(Program));

            //soluciona el problema de ciclos infinitos al serializar objetos con propiedades de navegacion
            builder.Services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.ReferenceHandler =
                        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            builder.Services.AddRouting(options=> options.LowercaseUrls = true);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserService, UserService>();

            var app = builder.Build();

            using(var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                
                // Aplicar migraciones pendientes al iniciar la aplicación
                dbContext.Database.Migrate(); 
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
