using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface IUserActionRepository
    {
        Task<IEnumerable<UserActions>> GetAll();
        Task<UserActions> Create(UserActions item);
    }
}
