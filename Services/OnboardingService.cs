using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Exceptions;
using elearning_platform.Models;
using elearning_platform.Repo;

namespace elearning_platform.Services
{
    public class OnboardingService : IOnboardingService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly ITutorRepo _tutorRepo;
        private readonly IClaimRepo _claimRepo;
        private readonly IStudentRepo _studentRepo;
        public OnboardingService(IMapper mapper, IUserRepo userRepo, ITutorRepo tutorRepo, IClaimRepo claimRepo, IStudentRepo studentRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _claimRepo = claimRepo;
            _tutorRepo = tutorRepo;
            _studentRepo = studentRepo;
        }

        public User SignupUser(SignupDTO signupDTO)
        {
            var userModel = _mapper.Map<SignupDTO, User>(signupDTO);
            var existingUser = _userRepo.GetUserByEmail(userModel.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }
            var user = _userRepo.CreateUser(userModel);
            _userRepo.SaveChanges();
            return userModel;
        }

        public Tutor SignupTutor(TutorSignupDTO signupDTO)
        {
            var userModel = SignupUser(signupDTO);
            var newTutor = new Tutor()
            {
                Uid = userModel.Uid,
                Verified = false
            };
            _tutorRepo.CreateTutor(newTutor);
            _claimRepo.AddClaimForTutor(newTutor);
            return newTutor;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepo.GetUsers();
        }

        public User setUserBan(Guid userId, bool banned)
        {
            var user = _userRepo.GetUserById(userId);
            user.Banned = banned;
            _userRepo.SaveUser(user);
            return user;
        }

        public User UpdateUserDetails(Guid userId, UpdateUserDetailsDTO userDTO)
        {
            var user = _userRepo.GetUserById(userId);
            if(user == null)
            {
                throw new BadRequestException("User does not exist");
            }
            var mappedUser = _mapper.Map<UpdateUserDetailsDTO, User>(userDTO, user);
            _userRepo.SaveUser(mappedUser);
            return mappedUser;
        }

        public Tutor SignupTutor(SignupDTO signupDTO)
        {
            throw new NotImplementedException();
        }

        public Student SignupStudent(StudentSignupDTO signupDTO)
        {
            var userModel = SignupUser(signupDTO);
            var newTutor = new Student()
            {
                Uid = userModel.Uid,
                EducationLevelId = signupDTO.EducationLevelId
            };
            _studentRepo.CreateStudent(newTutor);
            _claimRepo.AddClaimForStudent(newTutor);
            return newTutor;
        }
    }
}
