namespace BigDataCourse.Areas.Admin.Recommender.Mathematics
{
    public class Matrix
    {
        /// <summary>
        /// Calculate the dot product between two vectors
        /// </summary>
        public static double GetDotProduct(double[] matrixOne, double[] matrixTwo)
        {
            return matrixOne.Zip(matrixTwo, (a, b) => a * b).Sum();
        }
    }
}
