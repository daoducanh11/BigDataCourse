using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
    }
}
