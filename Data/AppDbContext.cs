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

    }
}