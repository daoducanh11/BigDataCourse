using BigDataCourse.Areas.Admin.Recommender.Parsers;

namespace BigDataCourse.Areas.Admin.Recommender.Abstractions
{
    public interface ISplitter
    {
        UserBehaviorDatabase TrainingDB { get; }

        UserBehaviorDatabase TestingDB { get; }
    }
}
