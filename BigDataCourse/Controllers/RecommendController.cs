using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models;
using BigDataCourse.Recommender.Abstractions;
using BigDataCourse.Recommender.Comparers;
using BigDataCourse.Recommender.Objects;
using BigDataCourse.Recommender.Parsers;
using BigDataCourse.Recommender.Raters;
using BigDataCourse.Recommender.Recommenders;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BigDataCourse.Controllers
{
    public class RecommendController : Controller
    {
        private readonly IUserActionRepository _userActionRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITagRepository _tagRepository;

        public RecommendController(IUserActionRepository userActionRepository, IArticleRepository articleRepository, IUserRepository userRepository, ITagRepository tagRepository)
        {
            _userActionRepository = userActionRepository;
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
        }
        public async Task<IActionResult> Index()
        {
            UserBehaviorDatabaseParser parser = new UserBehaviorDatabaseParser();
            //UserBehaviorDatabase db = await parser.LoadUserBehaviorDatabase();
            UserBehaviorDatabase db = new UserBehaviorDatabase();
            db.UserActions = await _userActionRepository.GetAll();
            db.Articles = await _articleRepository.GetAll();
            db.Users = await _userRepository.GetAll();
            db.Tags = await _tagRepository.GetAll();

            IRater rate = new LinearRater(-4, 2, 3, 1);
            IComparer compare = new CorrelationUserComparer();

            IRecommender recommender;
            recommender = new UserCollaborativeFilterRecommender(compare, rate, 50);
            if (System.IO.File.Exists("recommender.dat"))
            {
                try
                {
                    recommender.Load("recommender.dat");
                }
                catch
                {
                    recommender.Train(db);
                    recommender.Save("recommender.dat");
                }
            }
            else
            {
                recommender.Train(db);
                recommender.Save("recommender.dat");
            }

            int uId = (int)HttpContext.Session.GetInt32("_user");
            List<Suggestion> lst = recommender.GetSuggestions(uId, 10);

            List<int> listRes = new List<int>();
            foreach (Suggestion s in lst)
                listRes.Add(s.ArticleID);
            ViewBag.listTag = await _tagRepository.GetAll();
            return View(await _articleRepository.GetByListID(listRes));
        }
    }
}
