using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BigDataCourse.Areas.Admin.Models
{
    public class Articles
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public int ArticleID { get; set; }
        public string ArticleName { get; set; }
        public List<string> ArticleTags { get; set; }
    }
}
