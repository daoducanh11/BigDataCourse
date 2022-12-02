using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface IUserActionRepository
    {
        Task<List<UserAction>> GetAll();
        Task<UserAction> Create(UserAction item);
        Task<IEnumerable<UserAction>> GetByUserID(int id, string action, int page, int pageSize);
        Task<IEnumerable<UserAction>> GetByArticleID(int id, string action, int page, int pageSize);
    }
}
