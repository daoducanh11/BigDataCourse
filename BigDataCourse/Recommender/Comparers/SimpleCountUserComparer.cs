using BigDataCourse.Recommender.Abstractions;

namespace BigDataCourse.Recommender.Comparers
{
    public class SimpleCountUserComparer : IComparer
    {
        public double CompareVectors(double[] userFeaturesOne, double[] userFeaturesTwo)
        {
            double count = 0.0;
            for (int i = 0; i < userFeaturesOne.Length; i++)
            {
                if (userFeaturesOne[i] != 0 && userFeaturesTwo[i] != 0)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
