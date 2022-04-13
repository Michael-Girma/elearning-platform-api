using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public enum MfaAuthResult
    {
        Authenticated,
        ExpiredCode,
        InvalidCode
    }

    public interface IMfaRepo
    {


        Task<Mfa> GenerateMfaAsync(User user);

        Task<MfaAuthResult> CheckMfaAsync(User user, int value);

        bool SaveChanges();
    }
}