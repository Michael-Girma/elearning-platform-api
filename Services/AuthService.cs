using System.Net.Mail;
using elearning_platform.Configs;
using elearning_platform.DTO;
using elearning_platform.Exceptions;
using elearning_platform.Models;
using elearning_platform.Repo;

namespace elearning_platform.Services
{
    public class AuthService : IAuthService
    {
        private readonly SMTPConfig _smtpConfig;
        private readonly IEmailService _emailService;
        private readonly IUserRepo _userRepo;

        public AuthService(SMTPConfig smtpConfig, IEmailService emailService, IUserRepo userRepo)
        {
            _smtpConfig = smtpConfig;
            _emailService = emailService;
            _userRepo = userRepo;
        }

        public async Task<bool> RequestReset(string email)
        {
            var user = _userRepo.GetUserByEmail(email);
            if(user != null)
            {
                var token = Guid.NewGuid().ToString();
                var passwordResetToken = new ResetPasswordToken(){
                    Used = false,
                    Token = token,
                    UserId = user.Uid
                };
                _userRepo.SaveResetToken(passwordResetToken);
                return _emailService.SendEmail(user.Email, $"Click on this <a href=\"http://localhost:4200/reset-password?token={token}\">Link</a> to reset your password", "Password Reset Link", true);
            }
            return false;
        }

        public async Task<User> ResetPassword(ResetPasswordDTO resetDTO)
        {
            var token = _userRepo.GetResetPasswordToken(resetDTO.Token);
            if(token == null || token.Used || token.ExpiresAt < DateTime.UtcNow)
            {
                throw new BadRequestException("Invalid Token Used");
            }else
            {
                return _userRepo.ResetPassword(resetDTO);
            }
        }

        public Task<bool> SendMfaAsync(User user, Mfa mfa)
        {
            var body = $"Here's your new code: {mfa.PinCode}";
            var subject = "Multi-Factor Auth";
            var success = _emailService.SendEmail(user.Email, body, subject);
            return Task.FromResult(success);
        }

        public User ChangePassword(User user, ChangePasswordDTO changePasswordDTO)
        {
            var loginDTO = new LoginDTO(){
                Email = user.Email,
                Password = changePasswordDTO.OldPassword
            };
            var savedUser = _userRepo.AuthUser(loginDTO);
            if(savedUser == null || changePasswordDTO.NewPassword != changePasswordDTO.ConfirmPassword)
            {
                throw new BadRequestException("Wrong Credentials");
            }
            savedUser.Password = _userRepo.HashPassword(changePasswordDTO.NewPassword);
            _userRepo.SaveUser(savedUser);
            return savedUser;
        }
    }
}
