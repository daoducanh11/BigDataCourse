using BigDataCourse.Areas.Admin.Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BigDataCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        public ArticleController(IArticleRepository articleRepository)
        {
            this._articleRepository = articleRepository;
        }

        public async Task<IActionResult> Index(string name = "", int page = 1, int pageSize = 10)
        {
            ViewBag.pageOffset = (page - 1) * pageSize;
            ViewBag.name = name;
            return View(await _articleRepository.Get(name, page, pageSize));
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(string tagName)
        //{
        //    await _articleRepository.Create(User(tagName));
        //    return RedirectToAction("Index", "Tag");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Update(string tagId, string tagName)
        //{
        //    await _articleRepository.Update(tagId, new Models.Tag(tagName));
        //    return RedirectToAction("Index", "Tag");
        //}

        public async Task<IActionResult> Delete(string id)
        {
            await _articleRepository.Delete(id);
            return RedirectToAction("Index", "Tag");
        }
    }
}
