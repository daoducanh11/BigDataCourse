using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAll();
        Task<IEnumerable<Article>> Get(string name, int page, int pageSize);
        Task<int> GetNewId();
        Task<Article> GetById(string id);
        Task<Article> Create(Article item);
        Task<bool> Update(string id, Article item);
        Task<bool> Delete(string id);
    }
}
