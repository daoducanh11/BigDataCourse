using BigDataCourse.Areas.Admin.Authorization;
using BigDataCourse.Areas.Admin.Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BigDataCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizationAdmin]
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
        public async Task<IActionResult> Create(string tagName)
        {
            await _tagRepository.Create(new Models.Tag(tagName));
            return RedirectToAction("Index", "Tag");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string tagId, string tagName)
        {
            await _tagRepository.Update(tagId, new Models.Tag(tagName));
            return RedirectToAction("Index", "Tag");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _tagRepository.Delete(id);
            return RedirectToAction("Index", "Tag");
        }
    }
}
