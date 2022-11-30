﻿using BigDataCourse.Areas.Admin.Models;
using BigDataCourse.Areas.Admin.Recommender.Objects;
using System.Runtime.Serialization.Formatters.Binary;

namespace BigDataCourse.Areas.Admin.Recommender.Parsers
{
    public class UserBehaviorDatabase
    {
        public List<Tag> Tags { get; set; }

        public List<Article> Articles { get; set; }

        public List<User> Users { get; set; }

        public List<UserAction> UserActions { get; set; }

        public UserBehaviorDatabase()
        {
            Tags = new List<Tag>();
            Articles = new List<Article>();
            Users = new List<User>();
            UserActions = new List<UserAction>();
        }

        public List<ArticleTag> GetArticleTagLinkingTable()
        {
            List<ArticleTag> articleTags = new List<ArticleTag>();

            foreach (Article article in Articles)
            {
                foreach (Tag tag in article.Tags)
                {
                    articleTags.Add(new ArticleTag(article.ArticleID, tag.Name));
                }
            }

            return articleTags;
        }

        public UserBehaviorDatabase Clone()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;
                return (UserBehaviorDatabase)formatter.Deserialize(ms);
            }
        }
    }
}
