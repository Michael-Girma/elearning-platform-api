using elearning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace elearning_platform.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<EducationLevel> EducationLevels { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Mfa> MFAs { get; set; }

        public DbSet<InternalFileMetadata> InternalFiles { get; set; }

        public DbSet<Tutor> Tutors { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<TaughtSubject> TaughtSubjects { get; set; }

        public DbSet<TutorRequest> TutorRequests { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public DbSet<OnlineSession> OnlineSession { get; set; }

        public DbSet<PaymentOrder> PaymentOrders { get; set; }
    }
}