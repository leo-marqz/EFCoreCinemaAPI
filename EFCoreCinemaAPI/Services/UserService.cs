namespace EFCoreCinemaAPI.Services
{

    public interface IUserService
    {
        string GetUserById();
    }


    public class UserService : IUserService
    {
        public string GetUserById()
        {
            return "leomarqz";
        }
    }
}
