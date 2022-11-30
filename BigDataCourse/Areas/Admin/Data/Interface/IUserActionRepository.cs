using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface IUserActionRepository
    {
        Task<List<UserAction>> GetAll();
        Task<UserAction> Create(UserAction item);
    }
}
