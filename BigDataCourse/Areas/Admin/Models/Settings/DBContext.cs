using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BigDataCourse.Areas.Admin.Models.Settings
{
    public class DBContext
    {
        private readonly IMongoDatabase _database = null;

        public DBContext(IOptions<DbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Admins> Admins
        {
            get
            {
                return _database.GetCollection<Admins>("Admin");
            }
        }
        public IMongoCollection<Categories> Categories
        {
            get
            {
                return _database.GetCollection<Categories>("Category");
            }
        }
        public IMongoCollection<UserActions> UserActions
        {
            get
            {
                return _database.GetCollection<UserActions>("UserAction");
            }
        }

        public IMongoCollection<Articles> Articles
        {
            get
            {
                return _database.GetCollection<Articles>("Article");
            }
        }

        public IMongoCollection<Tags> Tags
        {
            get
            {
                return _database.GetCollection<Tags>("Tag");
            }
        }

        public IMongoCollection<Users> Users
        {
            get
            {
                return _database.GetCollection<Users>("User");
            }
        }
    }
}
