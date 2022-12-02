using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BigDataCourse.Areas.Admin.Models
{
    public class UserAction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string Action { get; set; }
        public int Day { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int ArticleID { get; set; }
        public string ArticleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserAction(int day, string action, int userid, string username, int articleid, string articlename)
        {
            Day = day;
            Action = action;
            UserID = userid;
            UserName = username;
            ArticleID = articleid;
            ArticleName = articlename;
        }

        public override string ToString()
        {
            return Day + "," + Action + "," + UserID + "," + UserName + "," + ArticleID + "," + ArticleName;
        }
    }
}
