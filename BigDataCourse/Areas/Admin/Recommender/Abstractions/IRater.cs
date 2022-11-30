using BigDataCourse.Areas.Admin.Models;

namespace BigDataCourse.Areas.Admin.Recommender.Abstractions
{
    public interface IRater
    {
        double GetRating(List<UserAction> actions);
    }
}
