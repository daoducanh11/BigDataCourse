using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Data.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Categories>> Get(int page, int pageSize);
    }
}
