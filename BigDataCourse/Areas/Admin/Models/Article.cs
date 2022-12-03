using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BigDataCourse.Areas.Admin.Models
{
    public class Article
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public int ArticleID { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int View { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public int Download { get; set; }
        public bool IsDeleted { get; set; }

        public List<Tag> Tags { get; set; }

        public Article()
        {
            
        }
        public Article(int id, string name, List<Tag> tags)
        {
            ArticleID = id;
            Name = name;
            Tags = tags;
        }

        public override string ToString()
        {
            return ArticleID + "," + Name + "," + Tags.Select(x => x.Name).Aggregate((c, n) => c + "," + n);
        }
    }
}
