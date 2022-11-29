using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models.Settings;
using BigDataCourse.Areas.Admin.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PagedList;

namespace BigDataCourse.Areas.Admin.Data
{
    public class ArticleRepository: IArticleRepository
    {
        private readonly DBContext _context = null;

        public ArticleRepository(IOptions<DbSettings> settings)
        {
            _context = new DBContext(settings);
        }

        public async Task<IEnumerable<Articles>> GetAll()
        {
            try
            {
                List<Articles> lst = await _context.Articles.Find(_ => true)
                    .ToListAsync();
                
                return lst;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
        public async Task<Articles> Create(Articles item)
        {
            try
            {
                await _context.Articles.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
            return item;
        }
    }
}
