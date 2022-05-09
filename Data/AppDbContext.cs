using elearning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace elearning_platform.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            // this.ChangeTracker.LazyLoadingEnabled = true;
            // this.ChangeTracker.
        }

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

        public DbSet<SessionOrder> SessionOrders { get; set; }

        public DbSet<SessionPaymentLink> SessionPaymentLinks { get; set; }

        public DbSet<PaymentDetail> PaymentDetails { get; set; }

        public DbSet<PaymentAccountDetail> PaymentAccountDetails { get; set; }
        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                // for entities that inherit from BaseEntity,
                // set UpdatedOn / CreatedOn appropriately
                if (entry.Entity is BaseEntity trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            // set the updated date to "now"
                            trackable.UpdatedOn = utcNow;

                            // mark property as "don't touch"
                            // we don't want to update on a Modify operation
                            entry.Property("CreatedOn").IsModified = false;
                            break;

                        case EntityState.Added:
                            // set both updated and created date to "now"
                            trackable.CreatedOn = utcNow;
                            trackable.UpdatedOn = utcNow;
                            break;
                    }
                }
            }
        }
    }
}