using elearning_platform.Models;
using elearning_platform.Repo;


namespace elearning_platform.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IStudentRepo _studentRepo;
        private readonly IAdminRepo _adminRepo;
        private bool? _isAdmin;

        public User User { get; set; }

        public CurrentUserService(IUserRepo userRepo, IStudentRepo studentRepo, IAdminRepo adminRepo)
        {
            _userRepo = userRepo;
            _studentRepo = studentRepo;
            _adminRepo = adminRepo;
        }
        public Student? GetStudent()
        {
            if (User != null)
            {
                return _studentRepo.GetStudentByUid(User.Uid);
            }
            return null;
        }



        public bool isAdmin
        {
            get
            {
                if (_isAdmin != null)
                {
                    return (bool)_isAdmin;
                }
                else if (User != null)
                {
                    var admin = _adminRepo.GetAdminByUid(User.Uid);
                    if (admin != null)
                    {
                        _isAdmin = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
        }

        public void SetUser(int uid)
        {
            User = _userRepo.GetUserById(uid);
        }
    }
}