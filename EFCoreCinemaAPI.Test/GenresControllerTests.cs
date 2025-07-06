
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

        //necesito crear un test donde se reciba en nombre original de un genero y el nombre con el que se va a actualizar
        // esto va relacionado con la resolucion de conflictos de concurrencia optimista
        // el nombre original debe ser el mismo que el actual en la base de datos y ademas, si este es igual al nuevo nombre, no se 
        // actualiza, si es diferente, se actualiza el nombre del genero
        [TestMethod]
        public async Task Put_IfSendOriginalNameAndNewName_ShouldUpdateGenre()
        {
            //prepare
            var dbname = Guid.NewGuid().ToString();
            var context = CreateContext(dbname);
            var mapper = AutoMapperConfiguration();

            var genre = new Genre { Name = "Action" };
            context.Add(genre);
            await context.SaveChangesAsync();

            var context2 = CreateContext(dbname);
            var genresController = new GenresController(context2, mapper);

            //test
            //await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(
            //            () => genresController.Put(new DTOs.UpdateGenreDto()
            //            {
            //                Id = genre.Id,
            //                Name = "Action V2",
            //                OriginalName = "Action V1"
            //            })
            //        );

        }
    }
}