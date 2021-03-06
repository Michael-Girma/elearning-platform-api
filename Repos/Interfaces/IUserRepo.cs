using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface IUserRepo
    {
        User? GetUserById(int id, bool includeClaims = false);
        User? GetUserByUsername(string username, bool includeClaims = false);

        IEnumerable<User> GetUsers();

        bool DeleteUser(User user);

        bool SaveChanges();
    }
}