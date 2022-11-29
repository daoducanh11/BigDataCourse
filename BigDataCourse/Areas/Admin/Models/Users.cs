using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BigDataCourse.Areas.Admin.Models
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
