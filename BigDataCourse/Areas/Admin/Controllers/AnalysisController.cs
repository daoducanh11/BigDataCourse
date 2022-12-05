using BigDataCourse.Areas.Admin.Authorization;
using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace BigDataCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AuthorizationAdmin]
    public class AnalysisController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserActionRepository _userActionRepository;

        public AnalysisController(IArticleRepository articleRepository, IUserRepository userRepository, IUserActionRepository userActionRepository)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _userActionRepository = userActionRepository;
        }
        public async Task<IActionResult> Index()
        {
            List<long> numberOfUsers = new List<long>();
            List<long> tmps = new List<long>();
            List<Article> lst = await _articleRepository.GetAll();
            foreach(Article item in lst)
            {
                tmps.Add(await _userActionRepository.GetCountByArticleID(item.ArticleID));
            }

            List<string> lstLable = new List<string>();
            for(int i=0; i<45; i = i + 2)
            {
                lstLable.Add(i.ToString() + "-" + (i+1).ToString());
                numberOfUsers.Add(tmps.FindAll(x => x >= i && x < i + 2).Count());
            }
            ViewBag.lstLable = lstLable;
            return View(numberOfUsers);
        }

        public async Task<IActionResult> Index2()
        {
            List<long> numberOfArticles = new List<long>();
            List<long> tmps = new List<long>();
            List<Article> lst = await _articleRepository.GetAll();
            foreach (Article item in lst)
            {
                tmps.Add(await _userActionRepository.GetCountByArticleID(item.ArticleID));
            }

            List<string> lstLable = new List<string>();
            for (int i = 0; i < 51; i = i + 4)
            {
                lstLable.Add(i.ToString() + "-" + (i + 3).ToString());
                numberOfArticles.Add(tmps.FindAll(x => x >= i && x < i + 4).Sum());
            }
            ViewBag.lstLable = lstLable;
            return View(numberOfArticles);
        }
    }
}
