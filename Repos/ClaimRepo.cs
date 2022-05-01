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
            var newClaims = new UserClaim[] {
                new UserClaim()
                {
                    Claim = "STUDENT",
                    Value = student.StudentId.ToString(),
                    Uid = user.Uid
                }
            };
            AddClaimToExistingClaims(newClaims, user);
            _ctx.SaveChanges();
            return student;
        }

        public Tutor AddClaimForTutor(Tutor tutor)
        {
            var user = _ctx.Users.FirstOrDefault(entry => entry.Uid == tutor.Uid);
            if (user == null)
            {
                throw new Exception("User Does Not Exist");
            }
            var newClaims = new UserClaim[] {
                new UserClaim()
                {
                    Claim = "TUTOR",
                    Value = tutor.TutorId.ToString(),
                    Uid = user.Uid
                }
            };
            AddClaimToExistingClaims(newClaims, user);
            _ctx.SaveChanges();
            return tutor;
        }



        public ICollection<UserClaim> AddClaimToExistingClaims(UserClaim[] newClaims, User user)
        {
            user.Claims = user.Claims ?? new List<UserClaim>();
            var claims = from claim in user.Claims select claim.Claim;
            foreach (var newClaim in newClaims)
            {
                var existingClaim = claims.FirstOrDefault(claim => claim == newClaim.Claim);
                if (existingClaim == null)
                {
                    user.Claims.Add(newClaim);
                    _ctx.UserClaims.Add(newClaim);
                }
            }
            return user.Claims;
        }
    }
}