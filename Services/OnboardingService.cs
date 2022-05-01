using AutoMapper;
using elearning_platform.DTO;
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
        public OnboardingService(IMapper mapper, IUserRepo userRepo, ITutorRepo tutorRepo, IClaimRepo claimRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _claimRepo = claimRepo;
            _tutorRepo = tutorRepo;
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

        public Tutor SignupTutor(SignupDTO signupDTO)
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


    }
}
