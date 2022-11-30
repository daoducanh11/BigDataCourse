using BigDataCourse.Areas.Admin.Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BigDataCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        public TagController(ITagRepository tagRepository)
        {
            this._tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index(string name="", int page=1, int pageSize=10)
        {
            ViewBag.pageOffset = (page-1)*pageSize;
            ViewBag.name = name;
            return View(await _tagRepository.Get(name, page, pageSize));
        }

        [HttpPost]
        public ActionResult Create(string tagName)
        {

            return RedirectToAction("Index", "Tag");
        }
    }
}
