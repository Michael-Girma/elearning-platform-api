using elearning_platform.Data;
using elearning_platform.Models;
using System.Collections;

namespace elearning_platform.Repo
{
    public class ClaimRepo : IClaimRepo
    {
        private readonly AppDbContext _ctx;

        public ClaimRepo(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public Student AddClaimForStudent(Student student)
        {
            var user = _ctx.Users.FirstOrDefault(entry => entry.Uid == student.Uid);
            if (user == null)
            {
                throw new Exception("User Does Not Exist");
            }
            user.Claims = user?.Claims ?? new List<UserClaim>();
            var claims = from claim in user.Claims select claim.Claim;
            var studentClaim = claims.FirstOrDefault(claim => claim == "STUDENT");
            if (studentClaim == null)
            {
                var newClaim = new UserClaim()
                {
                    Claim = "STUDENT",
                    Value = student.StudentId.ToString(),
                    Uid = user.Uid
                };
                user.Claims.Add(newClaim);
                _ctx.UserClaims.Add(newClaim);
            }
            return student;
        }
    }
}