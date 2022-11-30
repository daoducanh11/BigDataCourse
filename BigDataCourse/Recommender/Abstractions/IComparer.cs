namespace BigDataCourse.Recommender.Abstractions
{
    public interface IComparer
    {
        double CompareVectors(double[] userFeaturesOne, double[] userFeaturesTwo);
    }
}
