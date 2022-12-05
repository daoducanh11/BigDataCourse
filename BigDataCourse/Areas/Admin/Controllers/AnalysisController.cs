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
        private readonly ITagRepository _tagRepository;
        private readonly IUserActionRepository _userActionRepository;

        public AnalysisController(IArticleRepository articleRepository, ITagRepository tagRepository, IUserActionRepository userActionRepository)
        {
            _articleRepository = articleRepository;
            _tagRepository = tagRepository;
            _userActionRepository = userActionRepository;
        }
        public async Task<IActionResult> Index()
        {
            List<int> numberOfArticles = new List<int>();
            List<int> numberOfUsers = new List<int>();
            List<long> tmps = new List<long>();
            List<Article> lst = await _articleRepository.GetAll();
            foreach(Article item in lst)
            {
                tmps.Add(await _userActionRepository.GetCountByArticleID(item.ArticleID));
            }
            tmps.OrderByDescending(x => x);
            return View();
        }
    }
}
