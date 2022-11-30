namespace BigDataCourse.Areas.Admin.Recommender.Objects
{
    public class ScoreResults
    {
        public double RootMeanSquareDifference { get; set; }

        public ScoreResults(double rmsd)
        {
            RootMeanSquareDifference = rmsd;
        }
    }
}
