using BigDataCourse.Areas.Admin.Recommender.Abstractions;

namespace BigDataCourse.Areas.Admin.Recommender.Comparers
{
    public class RootMeanSquareUserComparer : IComparer
    {
        public double CompareVectors(double[] userFeaturesOne, double[] userFeaturesTwo)
        {
            double score = 0.0;

            for (int i = 0; i < userFeaturesOne.Length; i++)
            {
                score += Math.Pow(userFeaturesOne[i] - userFeaturesTwo[i], 2);
            }

            // Higher numbers indicate closer similarity
            return -Math.Sqrt(score / userFeaturesOne.Length);
        }
    }
}
