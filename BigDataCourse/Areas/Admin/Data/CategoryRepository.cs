using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models;
using BigDataCourse.Areas.Admin.Models.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PagedList;

namespace BigDataCourse.Areas.Admin.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DBContext _context = null;

        public CategoryRepository(IOptions<DbSettings> settings)
        {
            _context = new DBContext(settings);
        }

        public async Task<IEnumerable<Categories>> Get(int page, int pageSize)
        {
            try
            {
                List<Categories> lst = await _context.Categories.Find(x => x.IsDeleted == false)
                    .ToListAsync();
                if (lst.Count > 0)
                    return lst.ToPagedList<Categories>(page, pageSize);
                return new List<Categories>();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
