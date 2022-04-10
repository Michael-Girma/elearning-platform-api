using elearning_platform.Data;
using elearning_platform.Models;
using System.Security.Cryptography;


namespace elearning_platform.Repo
{
    public class MfaRepo : IMfaRepo
    {
        private readonly AppDbContext _ctx;
        private int EXPIRE_TIME = 300;

        public MfaRepo(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public Task<bool> CheckMfaAsync(User user, int value)
        {
            var existingCode = _ctx.MFAs.SingleOrDefault(code => code.PinCode == value && code.Uid == user.Uid);

            if (existingCode != null && existingCode.ExpiresAt > DateTime.UtcNow)
            {
                return Task.FromResult<bool>(true);
            }
            return Task.FromResult<bool>(false);

        }

        public bool SaveChanges()
        {
            return _ctx.SaveChanges() >= 0;
        }
        public Task<Mfa> GenerateMfaAsync(User user)
        {
            var mfaCode = RandomNumberGenerator.GetInt32(9999);
            var mfa = new Mfa()
            {
                ExpiresAt = DateTime.Now.AddSeconds(EXPIRE_TIME).ToUniversalTime(),
                Uid = user.Uid,
                PinCode = mfaCode,
                Iat = DateTime.Now.ToUniversalTime()
            };
            var success = _ctx.MFAs.Add(mfa);
            return Task.FromResult<Mfa>(mfa);
        }
    }
}