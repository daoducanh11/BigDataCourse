using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAll();
        Task<IEnumerable<Tag>> Get(string name, int page, int pageSize);
    }
}
