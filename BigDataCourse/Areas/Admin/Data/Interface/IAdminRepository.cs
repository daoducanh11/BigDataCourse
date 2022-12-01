using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface IAdminRepository
    {
        Task<Models.Admin> Login(string userName, string password);
    }
}
