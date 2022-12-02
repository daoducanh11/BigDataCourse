using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface IUserRepository
    {
        Task<User> Login(string userName, string password);
        Task<List<User>> GetAll();
        Task<IEnumerable<User>> Get(string name, string phoneNumber, int page, int pageSize);
        Task<int> GetNewId();
        Task<User> Create(User item);
        Task<bool> Update(string id, User item);
        Task<bool> Delete(string id);
        Task<bool> IsExitPhonenumber(string phoneNumber);
        Task<bool> IsExitEmail(string email);
    }
}
