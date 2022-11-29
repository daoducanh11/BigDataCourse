using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Articles>> GetAll();
        Task<Articles> Create(Articles item);
    }
}
