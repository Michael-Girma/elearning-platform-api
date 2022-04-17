using elearning_platform.Data;
using elearning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace elearning_platform.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _ctx;

        public UserRepo(AppDbContext context)
        {
            _ctx = context;
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public User? GetUserById(int uid, bool includeClaims = false)
        {
            var user = includeClaims ? _ctx.Users.Include(u => u.Claims) : _ctx.Users.AsQueryable();
            return user.SingleOrDefault(u => u.Uid == uid);
        }

        public User? GetUserByUsername(string username, bool includeClaims = false)
        {
            var user = includeClaims ? _ctx.Users.Include(u => u.Claims) : _ctx.Users.AsQueryable();
            return user.SingleOrDefault(u => u.Username == username);
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _ctx.Users.ToList();
            return users;
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}