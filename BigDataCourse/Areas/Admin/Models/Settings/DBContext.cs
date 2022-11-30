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
        public IMongoCollection<UserAction> UserActions
        {
            get
            {
                return _database.GetCollection<UserAction>("UserAction");
            }
        }

        public IMongoCollection<Article> Articles
        {
            get
            {
                return _database.GetCollection<Article>("Article");
            }
        }

        public IMongoCollection<Tag> Tags
        {
            get
            {
                return _database.GetCollection<Tag>("Tag");
            }
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("User");
            }
        }
    }
}
