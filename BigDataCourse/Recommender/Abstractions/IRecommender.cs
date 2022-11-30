﻿using BigDataCourse.Recommender.Objects;
using BigDataCourse.Recommender.Parsers;

namespace BigDataCourse.Recommender.Abstractions
{
    public interface IRecommender
    {
        void Train(UserBehaviorDatabase db);

        List<Suggestion> GetSuggestions(int userId, int numSuggestions);

        double GetRating(int userId, int articleId);

        void Save(string file);

        void Load(string file);
    }
}
