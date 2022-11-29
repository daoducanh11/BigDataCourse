using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Index(Admins ad)
        {
            if (await _adminRepository.Login(ad.UserName, ad.Password) != null)
                return RedirectToAction("Index", "News");
            return View(ad);
        }

        public async Task<IActionResult> Add()
        {
            string[] rows = tmp.Split("\r\n");
            foreach (string row in rows)
            {
                string[] strs = row.Split(",");
                UserActions u = new UserActions();
                u.Day = new DateTime(2022,11, int.Parse(strs[0])).AddHours(18);
                u.Action = strs[1];
                u.UserID = int.Parse(strs[2]);
                u.UserName = strs[3];
                u.ArticleID = int.Parse(strs[4]);
                u.ArticleName = strs[5];
                await _userActionRepository.Create(u);
            }
            
            return View();
        }

        public async Task<IActionResult> Add2()
        {
            string[] rows = tmp.Split("\r\n");
            foreach (string row in rows)
            {
                string[] strs = row.Split(",");
                Articles u = new Articles();
                u.ArticleID = int.Parse(strs[0]);
                u.ArticleName = strs[1];
                List<string> list = new List<string>();
                for (int i=2; i<strs.Length; i++)
                {
                    list.Add(strs[i].Trim());
                }
                u.ArticleTags = list;
                await _articleRepository.Create(u);
            }

            return View();
        }

        public string tmp = "";
    }
}
