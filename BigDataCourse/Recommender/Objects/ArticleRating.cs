namespace BigDataCourse.Recommender.Objects
{
    public class ArticleRating
    {
        public int ArticleID { get; set; }

        public double Rating { get; set; }

        public ArticleRating(int articleId, double rating)
        {
            ArticleID = articleId;
            Rating = rating;
        }
    }
}
