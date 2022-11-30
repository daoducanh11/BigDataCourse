using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Recommender.Abstractions
{
    public interface IRater
    {
        double GetRating(List<UserAction> actions);
    }
}
