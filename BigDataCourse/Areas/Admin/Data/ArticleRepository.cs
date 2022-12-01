using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models.Settings;
using BigDataCourse.Areas.Admin.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using X.PagedList;
using MongoDB.Bson;

namespace BigDataCourse.Areas.Admin.Data
{
    public class ArticleRepository: IArticleRepository
    {
        private readonly DBContext _context = null;

        public ArticleRepository(IOptions<DbSettings> settings)
        {
            _context = new DBContext(settings);
        }

        public async Task<List<Article>> GetAll()
        {
            try
            {
                List<Article> lst = await _context.Articles.Find(_ => true)
                    .ToListAsync();
                
                return lst;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
        
        public async Task<IEnumerable<Article>> Get(string name, int page, int pageSize)
        {
            try
            {
                List<Article> lst = await _context.Articles.Find(item => item.Name.Contains(name))
                    .ToListAsync();
                return lst.ToPagedList<Article>(page, pageSize);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<int> GetNewId()
        {
            try
            {
                List<Article> lst = await _context.Articles.Find(_ => true)
                    .SortByDescending(item => item.ArticleID)
                    .Limit(1)
                    .ToListAsync();
                return lst[0].ArticleID + 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public async Task<Article> Create(Article item)
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

        public async Task<bool> Update(string id, Article item)
        {
            ObjectId internalId = GetInternalId(id);
            var filter = Builders<Article>.Filter.Eq(s => s._id, internalId);
            var update = Builders<Article>.Update
                            .Set(s => s.Name, item.Name);
            try
            {
                UpdateResult actionResult = await _context.Articles.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
        public async Task<bool> Delete(string id)
        {
            ObjectId internalId = GetInternalId(id);
            var filter = Builders<Article>.Filter.Eq(s => s._id, internalId);
            try
            {
                DeleteResult actionResult = await _context.Articles.DeleteOneAsync(filter);

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // Try to convert the Id to a BSonId value
        private ObjectId GetInternalId(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }
    }
}
