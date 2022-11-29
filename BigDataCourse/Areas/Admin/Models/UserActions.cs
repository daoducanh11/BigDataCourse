using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BigDataCourse.Areas.Admin.Models
{
    public class UserActions
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string Action { get; set; }
        public DateTime Day { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int ArticleID { get; set; }
        public string ArticleName { get; set; }
    }
}
