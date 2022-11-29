using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAll();
    }
}
