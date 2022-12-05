﻿using BigDataCourse.Recommender.Abstractions;
using BigDataCourse.Recommender.Objects;
using BigDataCourse.Recommender.Parsers;

namespace BigDataCourse.Recommender.Recommenders
{
    public class RandomRecommender : IRecommender
    {
        private Random rand;

        public RandomRecommender()
        {
            rand = new Random();
        }

        public void Train(UserBehaviorDatabase db)
        {
        }

        public double GetRating(int userId, int articleId)
        {
            return rand.NextDouble() * 5.0;
        }

        public List<Suggestion> GetSuggestions(int userId, int numSuggestions)
        {
            List<Suggestion> suggestions = new List<Suggestion>();

            for (int i = 0; i < numSuggestions; i++)
            {
                suggestions.Add(new Suggestion(userId, rand.Next(1, 3000), rand.NextDouble() * 5.0));
            }

            return suggestions;
        }

        public void Load(string file)
        {
            throw new NotImplementedException();
        }

        public void Save(string file)
        {
            throw new NotImplementedException();
        }

        public List<ArticleRating> GetNearestNeighbors(int id, int v)
        {
            throw new NotImplementedException();
        }
    }

}
