using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface IUserActionRepository
    {
        Task<List<UserAction>> GetAll();
        Task<long> GetCountByArticleID(int id, string action);
        Task<bool> IsActionByUserID(int userId, int articleId, string action);
        Task<long> GetCountByArticleID(int id);
        Task<UserAction> Create(UserAction item);
        Task<IEnumerable<UserAction>> GetByUserID(int id, string action, int page, int pageSize);
        Task<IEnumerable<UserAction>> GetByArticleID(int id, string action, int page, int pageSize);
    }
}
