using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface IMfaRepo
    {
        Task<Mfa> GenerateMfaAsync(User user);

        Task<bool> CheckMfaAsync(User user, int value);

        bool SaveChanges();
    }
}