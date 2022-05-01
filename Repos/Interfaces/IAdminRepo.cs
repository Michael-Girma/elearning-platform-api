using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface IAdminRepo
    {
        Admin? GetAdminByAdminId(Guid adminId);

        Admin? GetAdminByUid(Guid uid);

        bool SaveChanges();
    }
}