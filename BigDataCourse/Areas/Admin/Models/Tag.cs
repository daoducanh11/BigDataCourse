using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BigDataCourse.Areas.Admin.Models
{
    public class Tag
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public Tag(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
