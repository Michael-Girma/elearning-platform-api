using elearning_platform.Data;
using elearning_platform.Models;
using System.Security.Cryptography;


namespace elearning_platform.Repo
{
    public class MfaRepo : IMfaRepo
    {
        private readonly AppDbContext _ctx;
        private int EXPIRE_TIME = 300000; //TODO: move this to auth config and parse in cs file

        public MfaRepo(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<MfaAuthResult> CheckMfaAsync(User user, int value)
        {
            var existingCode = _ctx.MFAs.SingleOrDefault(code => code.PinCode == value && code.Uid == user.Uid);

            if (existingCode == null)
            {
                return MfaAuthResult.InvalidCode;
            }
            else if (existingCode.ExpiresAt.CompareTo(DateTime.UtcNow) < 1)
            {
                return MfaAuthResult.ExpiredCode;
            }
            else
            {
                return MfaAuthResult.Authenticated;
            }
        }

        public bool SaveChanges()
        {
            return _ctx.SaveChanges() >= 0;
        }
        public Task<Mfa> GenerateMfaAsync(User user)
        {
            var mfaCode = RandomNumberGenerator.GetInt32(8999) + 1000;
            var mfa = new Mfa()
            {
                ExpiresAt = DateTime.UtcNow.AddSeconds(EXPIRE_TIME),
                Uid = user.Uid,
                PinCode = mfaCode,
                Iat = DateTime.Now.ToUniversalTime()
            };
            var success = _ctx.MFAs.Add(mfa);
            _ctx.SaveChanges();
            return Task.FromResult<Mfa>(mfa);
        }
    }
}