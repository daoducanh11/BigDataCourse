using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface IAdminRepository
    {
        Task<Admins> Login(string userName, string password);
    }
}
