using EFCoreCinemaAPI.Services;

namespace EFCoreCinemaAPI.Test
{
    [TestClass]
    public sealed class UserServiceTests
    {
        //Estructura del Nombre del Test: Metodoa Probar _ Lo que se Espera _ Test

        [TestMethod]
        public void GetUserId_NotNullOrEmpty_Test()
        {
            // Prepare
            var userService = new UserService();

            // test
            var userId = userService.GetUserById();

            // verify
            Assert.IsNotNull(userId, "User ID should not be null.");
            Assert.AreNotEqual(string.Empty, userId, "User ID should not be an empty string.");
        }
    }
}
