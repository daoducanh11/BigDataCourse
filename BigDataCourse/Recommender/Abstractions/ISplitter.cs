using BigDataCourse.Recommender.Parsers;

namespace BigDataCourse.Recommender.Abstractions
{
    public interface ISplitter
    {
        UserBehaviorDatabase TrainingDB { get; }

        UserBehaviorDatabase TestingDB { get; }
    }
}
