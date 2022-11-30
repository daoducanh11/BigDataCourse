using BigDataCourse.Areas.Admin.Recommender.Abstractions;

namespace BigDataCourse.Areas.Admin.Recommender.Comparers
{
    public class CoRatedCosineUserComparer : IComparer
    {
        public double CompareVectors(double[] userFeaturesOne, double[] userFeaturesTwo)
        {
            double sumProduct = 0.0;
            double sumOneSquared = 0.0;
            double sumTwoSquared = 0.0;

            for (int i = 0; i < userFeaturesOne.Length; i++)
            {
                // Only compare articles rated by both users
                if (userFeaturesOne[i] != 0 && userFeaturesTwo[i] != 0)
                {
                    sumProduct += userFeaturesOne[i] * userFeaturesTwo[i];
                    sumOneSquared += Math.Pow(userFeaturesOne[i], 2);
                    sumTwoSquared += Math.Pow(userFeaturesTwo[i], 2);
                }
            }

            if (sumOneSquared > 0 && sumTwoSquared > 0)
            {
                return sumProduct / (Math.Sqrt(sumOneSquared) * Math.Sqrt(sumTwoSquared));
            }
            else
            {
                return double.NegativeInfinity;
            }
        }
    }
}
