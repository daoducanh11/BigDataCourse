using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models;
using BigDataCourse.Areas.Admin.Models.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using X.PagedList;

namespace BigDataCourse.Areas.Admin.Data
{
    public class UserActionRepository: IUserActionRepository
    {
        private readonly DBContext _context = null;

        public UserActionRepository(IOptions<DbSettings> settings)
        {
            _context = new DBContext(settings);
        }

        public async Task<List<UserAction>> GetAll()
        {
            try
            {
                List<UserAction> lst = await _context.UserActions.Find(_ => true)
                    .ToListAsync();

                return lst;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<UserAction>> GetByUserID(int id, string action, int page, int pageSize)
        {
            try
            {
                List<UserAction> lst = await _context.UserActions.Find(item => item.UserID.Equals(id) && item.Action.Contains(action))
                    .ToListAsync();
                return lst.ToPagedList<UserAction>(page, pageSize);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<UserAction>> GetByArticleID(int id, string action, int page, int pageSize)
        {
            try
            {
                List<UserAction> lst = await _context.UserActions.Find(item => item.ArticleID.Equals(id) && item.Action.Contains(action))
                    .ToListAsync();
                return lst.ToPagedList<UserAction>(page, pageSize);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<UserAction> Create(UserAction item)
        {
            try
            {
                await _context.UserActions.InsertOneAsync(item);
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
