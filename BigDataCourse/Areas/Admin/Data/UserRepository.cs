﻿using BigDataCourse.Areas.Admin.Models.Settings;
using BigDataCourse.Areas.Admin.Models;
using Microsoft.Extensions.Options;
using BigDataCourse.Areas.Admin.Data.Interface;
using MongoDB.Driver;
using MongoDB.Bson;
using X.PagedList;

namespace BigDataCourse.Areas.Admin.Data
{
    public class UserRepository: IUserRepository
    {
        private readonly DBContext _context = null;

        public UserRepository(IOptions<DbSettings> settings)
        {
            _context = new DBContext(settings);
        }

        public async Task<List<User>> GetAll()
        {
            try
            {
                List<User> lst = await _context.Users.Find(_ => true)
                    .ToListAsync();

                return lst;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<User>> Get(string name, int page, int pageSize)
        {
            try
            {
                List<User> lst = await _context.Users.Find(item => item.Name.Contains(name))
                    .ToListAsync();
                return lst.ToPagedList<User>(page, pageSize);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<User> Create(User item)
        {
            try
            {
                await _context.Users.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
            return item;
        }

        public async Task<bool> Update(string id, User item)
        {
            ObjectId internalId = GetInternalId(id);
            var filter = Builders<User>.Filter.Eq(s => s._id, internalId);
            var update = Builders<User>.Update
                            .Set(s => s.Name, item.Name);
            try
            {
                UpdateResult actionResult = await _context.Users.UpdateOneAsync(filter, update);

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
            var filter = Builders<User>.Filter.Eq(s => s._id, internalId);
            var update = Builders<User>.Update.Set(s => s.IsDeleted, true);
            try
            {
                UpdateResult actionResult = await _context.Users.UpdateOneAsync(filter, update);

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
