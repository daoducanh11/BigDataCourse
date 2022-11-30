namespace BigDataCourse.Recommender.Objects
{
    public class TestResults
    {
        public int TotalUsers { get; set; }

        public int UsersSolved { get; set; }

        public double AverageRecall { get; set; }

        public double AveragePrecision { get; set; }

        public TestResults(int totalUsers, int usersSolved, double averageRecall, double averagePrecision)
        {
            TotalUsers = totalUsers;
            UsersSolved = usersSolved;
            AverageRecall = averageRecall;
            AveragePrecision = averagePrecision;
        }
    }
}
