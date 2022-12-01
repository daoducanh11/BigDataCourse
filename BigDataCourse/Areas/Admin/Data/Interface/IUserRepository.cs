using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<IEnumerable<User>> Get(string name, int page, int pageSize);
        Task<User> Create(User item);
        Task<bool> Update(string id, User item);
        Task<bool> Delete(string id);
    }
}
