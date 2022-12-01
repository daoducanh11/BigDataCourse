using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using X.PagedList;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BigDataCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ITagRepository _tagRepository;

        private IHostingEnvironment _environment;
        public ArticleController(IArticleRepository articleRepository, ITagRepository tagRepository, IHostingEnvironment environment)
        {
            _articleRepository = articleRepository;
            _tagRepository = tagRepository;
            _environment = environment;
        }

        public async Task<IActionResult> Index(string name = "", int page = 1, int pageSize = 10)
        {
            ViewBag.pageOffset = (page - 1) * pageSize;
            ViewBag.name = name;
            return View(await _articleRepository.Get(name, page, pageSize));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.listTag = await _tagRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string Name, string Content, List<IFormFile> photo, IFormCollection data)
        {
            Article a = new Article();
            a.Name = data["Name"];
            a.Content = data["Content"];
            string tmp = data["tags"];
            string[] tags = tmp.Split(",");
            List<Tag> lstTag = new List<Tag>();
            foreach (string tag in tags)
                lstTag.Add(new Tag(tag));
            a.Tags = lstTag;
            foreach (IFormFile postedFile in photo)
            {
                string path = Path.Combine(this._environment.WebRootPath, "Uploads");
                string fileName = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    a.Image = fileName;
                }
            }
            a.CreatedDate = DateTime.Now;
            a.ArticleID = await _articleRepository.GetNewId();
            await _articleRepository.Create(a);
            return RedirectToAction("Index", "Article");
        }

        //[HttpPost]
        //public async Task<IActionResult> Update(string tagId, string tagName)
        //{
        //    await _articleRepository.Update(tagId, new Models.Tag(tagName));
        //    return RedirectToAction("Index", "Tag");
        //}

        public async Task<IActionResult> Delete(string id)
        {
            await _articleRepository.Delete(id);
            return RedirectToAction("Index", "Article");
        }
    }
}
