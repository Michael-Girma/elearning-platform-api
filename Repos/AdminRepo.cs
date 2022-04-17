using Microsoft.EntityFrameworkCore;

using elearning_platform.Data;
using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public class AdminRepo : IAdminRepo
    {
        private readonly AppDbContext _ctx;

        public AdminRepo(AppDbContext context)
        {
            _ctx = context;
        }


        public Admin? GetAdminByAdminId(int adminId)
        {
            var admin = _ctx.Admins.Include(a => a.User).FirstOrDefault(admin => admin.AdminId == adminId);
            return admin;
        }

        public Admin? GetAdminByUid(int uid)
        {
            var admin = _ctx.Admins.Include(a => a.User).FirstOrDefault(admin => admin.Uid == uid);
            return admin;
        }

        public bool SaveChanges()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}