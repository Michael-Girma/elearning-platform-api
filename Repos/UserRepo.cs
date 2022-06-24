using elearning_platform.Data;
using elearning_platform.Models;
using Microsoft.EntityFrameworkCore;
using elearning_platform.DTO;
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

        public User? GetUserById(Guid uid, bool includeClaims = false)
        {
            var user = includeClaims ? _ctx.Users.Include(u => u.Claims) : _ctx.Users.AsQueryable();
            return user.SingleOrDefault(u => u.Uid == uid);
        }



        public IEnumerable<User> GetUsers()
        {
            var users = _ctx.Users.ToList();
            return users;
        }

        public bool SaveChanges()
        {
            return _ctx.SaveChanges() > 0;
        }

        public User? GetUserByEmail(string emailAddress)
        {
            var user = _ctx.Users.FirstOrDefault(temp => temp.Email == emailAddress);
            return user;
        }

        public User CreateUser(User newUser)
        {
            newUser.Password = HashPassword(newUser.Password);
            _ctx.Users.Add(newUser);
            return newUser;
        }

        public User ResetPassword(ResetPasswordDTO resetDTO)
        {
            var passwordToken = _ctx.ResetPasswordTokens.FirstOrDefault(e => e.Token == resetDTO.Token);
            passwordToken.Used = true;
            passwordToken.User.Password = HashPassword(resetDTO.password);
            _ctx.SaveChanges();
            return passwordToken.User;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public User? AuthUser(LoginDTO loginDTO)
        {
            var user = _ctx.Users.Include(e => e.Claims).FirstOrDefault(tempUser => tempUser.Email == loginDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                return null;
            }
            return user;
        }

        public ResetPasswordToken GetResetPasswordToken(string token)
        {
            return _ctx.ResetPasswordTokens.FirstOrDefault(e => e.Token == token);
        }

        public ResetPasswordToken SaveResetToken(ResetPasswordToken token)
        {
            _ctx.ResetPasswordTokens.Add(token);
            _ctx.SaveChanges();
            return token;
        }

        public User SaveUser(User user)
        {
            var entity = _ctx.Users.FirstOrDefault(e => e.Uid == user.Uid);
            if (entity == null)
            {
                return null;
            }
            _ctx.Entry(entity).CurrentValues.SetValues(user);
            _ctx.SaveChanges();
            return entity;   
        }

        public User? GetUserByUsername(string username, bool includeClaims = false)
        {
            throw new NotImplementedException();
        }
    }
}