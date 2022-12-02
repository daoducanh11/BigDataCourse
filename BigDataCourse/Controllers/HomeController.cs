using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models;
using BigDataCourse.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BigDataCourse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITagRepository _tagRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IUserActionRepository _userActionRepository;

        public HomeController(ILogger<HomeController> logger, ITagRepository tagRepository, IArticleRepository articleRepository, IUserActionRepository userActionRepository)
        {
            _logger = logger;
            _tagRepository = tagRepository;
            _articleRepository = articleRepository;
            _userActionRepository = userActionRepository;
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
            Article a = await _articleRepository.GetByArticleId(id);
            if (HttpContext.Session.GetString("_user") != null)
            {
                User u = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_user"));
                UserAction ua = new UserAction(DateTime.Now.Day, "View", u.UserID, u.Name, a.ArticleID, a.Name);
                ua.CreatedAt = DateTime.Now;
            }
            return View(a);
        }

        public async Task<IActionResult> Interactive(string value)
        {
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