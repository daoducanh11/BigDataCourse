using BigDataCourse.Areas.Admin.Models.Settings;
using BigDataCourse.Areas.Admin.Models;
using Microsoft.Extensions.Options;
using BigDataCourse.Areas.Admin.Data.Interface;

namespace BigDataCourse.Areas.Admin.Data
{
    public class UserRepository: IUserRepository
    {
        private readonly DBContext _context = null;

        public UserRepository(IOptions<DbSettings> settings)
        {
            _context = new DBContext(settings);
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            try
            {
                List<Users> lst = await _context.Users.Find(_ => true)
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
