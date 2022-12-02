using BigDataCourse.Areas.Admin.Models.Settings;
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

        public async Task<User> Login(string userName, string password)
        {
            try
            {
                User u = await _context.Users
                                .Find(ad => ad.Name == userName && ad.Password == password && !ad.IsDeleted)
                                .FirstOrDefaultAsync();
                return u;
            }
            catch (Exception ex)
            {
                return null;
            }
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

        public async Task<IEnumerable<User>> Get(string name, string phoneNumber, int page, int pageSize)
        {
            try
            {
                List<User> lst = await _context.Users.Find(item => item.Name.Contains(name) && item.PhoneNumber.Contains(phoneNumber) && !item.IsDeleted)
                    .ToListAsync();
                return lst.ToPagedList<User>(page, pageSize);
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
                List<User> lst = await _context.Users.Find(_ => true)
                    .SortByDescending(item => item.UserID)
                    .Limit(1)
                    .ToListAsync();
                return lst[0].UserID + 1;
            }
            catch (Exception ex)
            {
                return 1;
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
                            .Set(s => s.Name, item.Name)
                            .Set(s => s.Password, item.Password)
                            .Set(s => s.Email, item.Email)
                            .Set(s => s.PhoneNumber, item.PhoneNumber);
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

        public async Task<bool> IsExitPhonenumber(string phoneNumber)
        {
            try
            {
                User u = await _context.Users
                                .Find(u => u.PhoneNumber == phoneNumber)
                                .FirstOrDefaultAsync();
                if(u != null)
                    return true;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
            return false;
        }

        public async Task<bool> IsExitEmail(string email)
        {
            try
            {
                User u = await _context.Users
                                .Find(u => u.Email == email)
                                .FirstOrDefaultAsync();
                if (u != null)
                    return true;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
            return false;
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
