using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BigDataCourse.Areas.Admin.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }

        public User(int id, string name)
        {
            UserID = id;
            Name = name;
        }

        public override string ToString()
        {
            return UserID + "," + Name;
        }
    }
}
