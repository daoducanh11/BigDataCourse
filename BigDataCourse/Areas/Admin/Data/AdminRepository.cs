using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models;
using BigDataCourse.Areas.Admin.Models.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BigDataCourse.Areas.Admin.Data
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DBContext _context = null;

        public AdminRepository(IOptions<DbSettings> settings)
        {
            _context = new DBContext(settings);
        }

        public async Task<Models.Admin> Login(string userName, string password)
        {
            try
            {
                Models.Admin ad = await _context.Admins
                                .Find(ad => ad.UserName == userName && ad.Password == password && !ad.IsDeleted)
                                .FirstOrDefaultAsync();
                return ad;
            }
            catch (Exception ex)
            {
                return null;    
            }
        }

    }
}
