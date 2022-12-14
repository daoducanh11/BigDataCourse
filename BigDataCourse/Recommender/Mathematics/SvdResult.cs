namespace BigDataCourse.Recommender.Mathematics
{
    public class SvdResult
    {
        public double AverageGlobalRating { get; private set; }
        public double[] UserBiases { get; private set; }
        public double[] ArticleBiases { get; private set; }
        public double[][] UserFeatures { get; private set; }
        public double[][] ArticleFeatures { get; private set; }

        public SvdResult(double averageGlobalRating, double[] userBiases, double[] articleBiases, double[][] userFeatures, double[][] articleFeatures)
        {
            AverageGlobalRating = averageGlobalRating;
            UserBiases = userBiases;
            ArticleBiases = articleBiases;
            UserFeatures = userFeatures;
            ArticleFeatures = articleFeatures;
        }
    }
}
