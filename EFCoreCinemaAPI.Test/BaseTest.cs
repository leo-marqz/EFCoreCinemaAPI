
using AutoMapper;
using EFCoreCinemaAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCinemaAPI.Test
{
    public class BaseTest
    {
        /// <summary>
        /// Creates a new instance of <see cref="ApplicationDbContext"/> configured to use an in-memory database.
        /// </summary>
        /// <remarks>This method is intended for scenarios where an in-memory database is required, such
        /// as testing or prototyping. The returned context is initialized with default services, including a <see
        /// cref="UserService"/> instance.</remarks>
        /// <param name="dbname">The name of the in-memory database to be used by the context. Must not be null or empty.</param>
        /// <returns>A new <see cref="ApplicationDbContext"/> instance configured with the specified in-memory database name.</returns>
        protected ApplicationDbContext CreateContext(string dbname)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbname)
                .Options;
            var userService = new UserService();

            var context = new ApplicationDbContext(options, userService, dbContextEvents: null);

            return context;
        }

        /// <summary>
        /// Coniguracion de AutoMapper para los test
        /// </summary>
        /// <returns></returns>
        protected IMapper AutoMapperConfiguration()
        {
            var config = new MapperConfiguration((options) =>
            {
                options.AddProfile(new AutoMapperProfile());
            });

            return config.CreateMapper();
        }
    }
}
