using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAll();
        Task<IEnumerable<Tag>> Get(string name, int page, int pageSize);
        Task<Tag> Create(Tag item);
        Task<bool> Update(string id, Tag item);
        Task<bool> Delete(string id);
    }
}
