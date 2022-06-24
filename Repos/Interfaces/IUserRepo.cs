using elearning_platform.Models;
using elearning_platform.DTO;
namespace elearning_platform.Repo
{
    public interface IUserRepo
    {
        User? GetUserById(Guid id, bool includeClaims = false);
        User? GetUserByUsername(string username, bool includeClaims = false);

        IEnumerable<User> GetUsers();

        bool DeleteUser(User user);
        // bool DeletePaymentDetails(User user);

        User? GetUserByEmail(string emailAddress);

        User? AuthUser(LoginDTO loginDTO);

        bool SaveChanges();
        User CreateUser(User newUser);

        ResetPasswordToken GetResetPasswordToken(string token);
        User ResetPassword(ResetPasswordDTO resetDTO);

        ResetPasswordToken SaveResetToken(ResetPasswordToken token);

        User SaveUser(User user);

        string HashPassword(string password);
    }
}