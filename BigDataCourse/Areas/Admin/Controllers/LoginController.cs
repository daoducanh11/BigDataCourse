using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BigDataCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserActionRepository _userActionRepository;
        private readonly IArticleRepository _articleRepository;
        public LoginController(IAdminRepository adminRepository, IUserActionRepository userActionRepository, IArticleRepository articleRepository)
        {
            this._adminRepository = adminRepository;
            this._userActionRepository = userActionRepository;
            this._articleRepository = articleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Models.Admin ad)
        {
            Models.Admin res = await _adminRepository.Login(ad.UserName, ad.Password);
            if (res != null)
            {
                HttpContext.Session.SetString("_user", JsonSerializer.Serialize(res));
                return RedirectToAction("Index", "News");
            }    
            return View(ad);
        }

        public IActionResult Loguot()
        {
            return RedirectToAction("Index", "Login");
        }

        //public async Task<IActionResult> Add()
        //{
        //    string[] rows = tmp.Split("\r\n");
        //    foreach (string row in rows)
        //    {
        //        string[] strs = row.Split(",");
        //        UserAction u = new UserAction();
        //        u.Day = new DateTime(2022,11, int.Parse(strs[0])).AddHours(18);
        //        u.Action = strs[1];
        //        u.UserID = int.Parse(strs[2]);
        //        u.UserName = strs[3];
        //        u.ArticleID = int.Parse(strs[4]);
        //        u.ArticleName = strs[5];
        //        await _userActionRepository.Create(u);
        //    }

        //    return View();
        //}

        public async Task<IActionResult> Add2()
        {
            string[] rows = tmp.Split("\r\n");
            foreach (string row in rows)
            {
                string[] strs = row.Split(",");
                List<Tag> list = new List<Tag>();
                for (int i = 2; i < strs.Length; i++)
                {
                    list.Add(new Tag(strs[i].Trim()));
                }
                Article u = new Article(int.Parse(strs[0]), strs[1], list);

                await _articleRepository.Create(u);
            }

            return View();
        }

        public string tmp = "";
    }
}
