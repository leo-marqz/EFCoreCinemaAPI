
using EFCoreCinemaAPI.Services;
using EFCoreCinemaAPI.Test.Mocks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EFCoreCinemaAPI.Test
{
    [TestClass]
    public class UpdateObservableCollectionTests
    {
        [TestMethod]
        public void Update_ShouldPopulateEntities_WhenTheyAreEmpty()
        {
            //prepare
            var mapper = new Mocks.Mapper();
            var updateObservableCollection = new UpdateObservableCollectionService(mapper);
            var entities = new ObservableCollection<WithId>();
            var dtos = new List<WithId>
            {
                new WithId { Id = 1 },
                new WithId { Id = 2 },
                new WithId { Id = 3 }
            };

            //test
            updateObservableCollection.Update(entities, dtos);

            //verify
            Assert.AreEqual(3, entities.Count, "The collection should contain 3 entities after update.");
            Assert.AreEqual(1, entities[0].Id, "The first entity should have Id 1.");
            Assert.AreEqual(2, entities[1].Id, "The second entity should have Id 2.");
        }

        [TestMethod]
        public void Update_ShouldRemoveEntities_WhenTheyAreNotInDtos()
        {
            //prepare
            var mapper = new Mocks.Mapper();
            var updateObservableCollection = new UpdateObservableCollectionService(mapper);
            var entities = new ObservableCollection<WithId>
            {
                new WithId { Id = 1 },
                new WithId { Id = 2 },
                new WithId { Id = 3 }
            };
            var dtos = new List<WithId>
            {
                new WithId { Id = 2 },
                new WithId { Id = 3 }
            };
            //test
            updateObservableCollection.Update(entities, dtos);
            //verify
            Assert.AreEqual(2, entities.Count, "The collection should contain 2 entities after update.");
            Assert.IsFalse(entities.Any(e => e.Id == 1), "Entity with Id 1 should be removed.");
        }

        [TestMethod]
        public void Update_ShouldUpdateEntities_WhenTheyExistInDtos()
        {
            //prepare
            var mapper = new Mocks.Mapper();
            var updateObservableCollection = new UpdateObservableCollectionService(mapper);
            var entities = new ObservableCollection<WithId>
            {
                new WithId { Id = 1},
                new WithId { Id = 2 }
            };
            var dtos = new List<WithId>
            {
                new WithId { Id = 1 },
                new WithId { Id = 2 }
            };
            //test
            updateObservableCollection.Update(entities, dtos);
            //verify
            Assert.AreEqual(2, entities.Count, "The collection should still contain 2 entities after update.");
            Assert.AreEqual(2, dtos.Count);
        }
    }
}
