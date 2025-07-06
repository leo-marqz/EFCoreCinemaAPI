
using EFCoreCinemaAPI.Controllers;
using EFCoreCinemaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreCinemaAPI.Test
{
    [TestClass]
    public class GenresControllerTests : BaseTest
    {
        [TestMethod]
        public async Task Post_IfSendTwoGenres_ShouldInsertInDb()
        {
            //prepare
            var dbname = Guid.NewGuid().ToString();
            var context = CreateContext(dbname);
            var genresControler = new GenresV2Controller(context, mapper: null);
            var genres = new Genre[]
            {
                new Genre { Name = "Action #1" },
                new Genre { Name = "Comedy #2" }
            };

            //test
            await genresControler.PostMany(genres);

            //verify
            var context2 = CreateContext(dbname);
            var genresFromDb = await context2.Genres.ToListAsync();

            Assert.AreEqual(2, genresFromDb.Count);
            Assert.AreEqual("Action #1", genresFromDb[0].Name);
            Assert.AreEqual("Comedy #2", genresFromDb[1].Name);

        }
    }
}
