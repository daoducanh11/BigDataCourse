using BigDataCourse.Areas.Admin.Models.Settings;
using BigDataCourse.Areas.Admin.Models;
using BigDataCourse.Areas.Admin.Data.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BigDataCourse.Areas.Admin.Data
{
    public class TagRepository: ITagRepository
    {
        private readonly DBContext _context = null;

        public TagRepository(IOptions<DbSettings> settings)
        {
            _context = new DBContext(settings);
        }

        public async Task<IEnumerable<Tags>> GetAll()
        {
            try
            {
                List<Tags> lst = await _context.Tags.Find(_ => true)
                    .ToListAsync();

                return lst;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
