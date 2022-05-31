using elearning_platform.Models;

namespace elearning_platform.DTO
{
    public class ReadLoginDTO
    {
        public string Message { get; set; }

        public ReadStudentDTO? Student { get; set; }

        public ReadAdminDTO? Admin { get; set; }

        public ReadTutorDTO? Tutor { get; set; }
        public ReadUserDTO User { get; set; }

        public JWTToken? Auth { get; set; }
    }
}