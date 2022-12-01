using BigDataCourse.Areas.Admin.Models.Settings;
using BigDataCourse.Areas.Admin.Models;
using BigDataCourse.Areas.Admin.Data.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Tag = BigDataCourse.Areas.Admin.Models.Tag;
using X.PagedList;
using MongoDB.Bson;

namespace BigDataCourse.Areas.Admin.Data
{
    public class TagRepository: ITagRepository
    {
        private readonly DBContext _context = null;

        public TagRepository(IOptions<DbSettings> settings)
        {
            _context = new DBContext(settings);
        }

        public async Task<List<Tag>> GetAll()
        {
            try
            {
                List<Tag> lst = await _context.Tags.Find(_ => true)
                    .ToListAsync();

                return lst;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<Tag>> Get(string name, int page, int pageSize)
        {
            try
            {
                List<Tag> lst = await _context.Tags.Find(item => item.Name.Contains(name) && !item.IsDeleted)
                    .ToListAsync();
                return lst.ToPagedList<Tag>(page, pageSize);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Tag> Create(Tag item)
        {
            try
            {
                await _context.Tags.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
            return item;
        }

        public async Task<bool> Update(string id, Tag item)
        {
            ObjectId internalId = GetInternalId(id);
            var filter = Builders<Tag>.Filter.Eq(s => s._id, internalId);
            var update = Builders<Tag>.Update
                            .Set(s => s.Name, item.Name);
            try
            {
                UpdateResult actionResult = await _context.Tags.UpdateOneAsync(filter, update);

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
            var filter = Builders<Tag>.Filter.Eq(s => s._id, internalId);
            var update = Builders<Tag>.Update.Set(s => s.IsDeleted, true);
            try
            {
                UpdateResult actionResult = await _context.Tags.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
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
