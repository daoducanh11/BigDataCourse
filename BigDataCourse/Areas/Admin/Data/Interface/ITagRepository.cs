using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAll();
    }
}
