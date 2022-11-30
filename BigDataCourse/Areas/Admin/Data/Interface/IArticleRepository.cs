using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAll();
        Task<Article> Create(Article item);
    }
}
