using elearning_platform.Models;

namespace elearning_platform.Repo
{
    public interface IAdminRepo
    {
        Admin? GetAdminByAdminId(int adminId);

        Admin? GetAdminByUid(int uid);

        bool SaveChanges();
    }
}