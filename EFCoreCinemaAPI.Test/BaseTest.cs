
using EFCoreCinemaAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCinemaAPI.Test
{
    public class BaseTest
    {
        protected ApplicationDbContext CreateContext(string dbname)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbname)
                .Options;
            var userService = new UserService();

            var context = new ApplicationDbContext(options, userService, dbContextEvents: null);

            return context;
        }
    }
}
