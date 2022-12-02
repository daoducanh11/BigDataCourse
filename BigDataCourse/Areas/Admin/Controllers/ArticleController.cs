using BigDataCourse.Areas.Admin.Authorization;
using BigDataCourse.Areas.Admin.Data;
using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using X.PagedList;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BigDataCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizationAdmin]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IUserActionRepository _userActionRepository;

        private IHostingEnvironment _environment;
        public ArticleController(IArticleRepository articleRepository, ITagRepository tagRepository, IHostingEnvironment environment, IUserActionRepository userActionRepository)
        {
            _articleRepository = articleRepository;
            _tagRepository = tagRepository;
            _environment = environment;
            _userActionRepository = userActionRepository;
        }

        public async Task<IActionResult> Index(string name = "", int page = 1, int pageSize = 10)
        {
            ViewBag.pageOffset = (page - 1) * pageSize;
            ViewBag.name = name;
            return View(await _articleRepository.Get(name, page, pageSize));
        }

        public async Task<IActionResult> ActionHistory(int id, string uAction = "", int page = 1, int pageSize = 20)
        {
            ViewBag.pageOffset = (page - 1) * pageSize;
            ViewBag.uAction = uAction;
            return View(await _userActionRepository.GetByArticleID(id, uAction, page, pageSize));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.listTag = await _tagRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(List<IFormFile> photo, IFormCollection data)
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

        public async Task<IActionResult> Update(string id)
        {
            ViewBag.listTag = await _tagRepository.GetAll();
            return View(await _articleRepository.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(List<IFormFile> photo, IFormCollection data)
        {
            Article a = new Article();
            a.Name = data["Name"];
            a.Content = data["Content"];
            a.Image = data["Image"];
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

        public async Task<IActionResult> Delete(string id)
        {
            await _articleRepository.Delete(id);
            return RedirectToAction("Index", "Article");
        }
    }
}
