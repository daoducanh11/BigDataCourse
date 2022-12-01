using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BigDataCourse.Areas.Admin.Models
{
    public class Admin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
    }
}
