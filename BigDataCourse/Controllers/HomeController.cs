using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models;
using BigDataCourse.Models;
using BigDataCourse.Recommender.Abstractions;
using BigDataCourse.Recommender.Comparers;
using BigDataCourse.Recommender.Objects;
using BigDataCourse.Recommender.Parsers;
using BigDataCourse.Recommender.Raters;
using BigDataCourse.Recommender.Recommenders;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BigDataCourse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITagRepository _tagRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IUserActionRepository _userActionRepository;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger, ITagRepository tagRepository, IArticleRepository articleRepository, IUserActionRepository userActionRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _tagRepository = tagRepository;
            _articleRepository = articleRepository;
            _userActionRepository = userActionRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index(string keyword = "")
        {
            ViewBag.keyword = keyword;
            ViewBag.listTag = await _tagRepository.GetAll();
            ViewBag.slide1 = await _articleRepository.Get(keyword, 1, 5);
            ViewBag.slide2 = await _articleRepository.Get(keyword, 2, 5);
            ViewBag.recent = await _articleRepository.Get(keyword, 3, 5);
            ViewBag.popular = await _articleRepository.Get(keyword, 3, 10);
            return View(await _articleRepository.Get(keyword, 4, 12));
        }

        public async Task<IActionResult> Detail(int id)
        {
            ViewBag.listTag = await _tagRepository.GetAll();
            ViewBag.countView = await _userActionRepository.GetCountByArticleID(id, "View");
            ViewBag.countUpVote = await _userActionRepository.GetCountByArticleID(id, "UpVote");
            ViewBag.countDownVote = await _userActionRepository.GetCountByArticleID(id, "DownVote");
            ViewBag.countDownload = await _userActionRepository.GetCountByArticleID(id, "Download");
            
            Article a = await _articleRepository.GetByArticleId(id);
            if (HttpContext.Session.GetString("_userName") != null)
            {
                int uId = (int)HttpContext.Session.GetInt32("_user");

                UserBehaviorDatabaseParser parser = new UserBehaviorDatabaseParser();
                UserBehaviorDatabase db = new UserBehaviorDatabase();
                db.UserActions = await _userActionRepository.GetAll();
                db.Articles = await _articleRepository.GetAll();
                db.Users = await _userRepository.GetAll();
                db.Tags = await _tagRepository.GetAll();

                IRater rate = new LinearRater(-4, 2, 3, 1);
                IComparer compare = new CorrelationUserComparer();

                IRecommender recommender;
                recommender = new ItemCollaborativeFilterRecommender(compare, rate, 50);
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

                List<ArticleRating> lst = recommender.GetNearestNeighbors(id, 10);

                List<int> listRes = new List<int>();
                foreach (ArticleRating s in lst)
                    listRes.Add(s.ArticleID);
                ViewBag.popular = await _articleRepository.GetByListID(listRes);
                ViewBag.isUpVote = await _userActionRepository.IsActionByUserID(uId, id, "UpVote");
                ViewBag.isDownVote = await _userActionRepository.IsActionByUserID(uId, id, "DownVote");
                ViewBag.isDownload = await _userActionRepository.IsActionByUserID(uId, id, "Download");

                string uName = HttpContext.Session.GetString("_userName");
                UserAction ua = new UserAction(DateTime.Now.Day, "View", uId, uName, a.ArticleID, a.Name);
                ua.CreatedAt = DateTime.Now;
                //_userActionRepository.Create(ua);
            }
            else
            {
                ViewBag.popular = await _articleRepository.Get("", 3, 10);
                ViewBag.isUpVote = false;
                ViewBag.isDownVote = false;
                ViewBag.isDownload = false;
            }    

            return View(a);
        }

        [HttpPost]
        public async Task<IActionResult> Interactive(int id, string value)
        {
            Article a = await _articleRepository.GetByArticleId(id);
            int uId = (int)HttpContext.Session.GetInt32("_user");
            string uName = HttpContext.Session.GetString("_userName");
            UserAction ua = new UserAction(DateTime.Now.Day, value, uId, uName, a.ArticleID, a.Name);
            ua.CreatedAt = DateTime.Now;
            _userActionRepository.Create(ua);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}