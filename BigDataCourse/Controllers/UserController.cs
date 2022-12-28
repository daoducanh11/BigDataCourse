using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Recommendations;

namespace BigDataCourse.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserActionRepository _userActionRepository;
        public UserController(IUserRepository userRepository, IUserActionRepository userActionRepository)
        {
            _userRepository = userRepository;
            _userActionRepository = userActionRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Name, string Password)
        {
            User res = await _userRepository.Login(Name, Password);
            if (res != null)
            {
                if (System.IO.File.Exists("recommender.dat"))
                {
                    System.IO.File.Delete("recommender.dat");
                }
                HttpContext.Session.SetInt32("_user", res.UserID);
                HttpContext.Session.SetString("_userName", res.Name);
                return RedirectToAction("Index", "Recommend");
            }
            ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_user");
            HttpContext.Session.Remove("_userName");
            return RedirectToAction("Login", "User");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User u)
        {
            if (await _userRepository.IsExitEmail(u.Email))
            {
                ModelState.AddModelError("", "Email này đã được đăng ký!");
                return View();
            }
            if (await _userRepository.IsExitPhonenumber(u.PhoneNumber))
            {
                ModelState.AddModelError("", "Số điện thoại này đã được đăng ký!");
                return View();
            }
            
            u.UserID = await _userRepository.GetNewId();
            await _userRepository.Create(u);
            return RedirectToAction("Login", "User");
        }
    }
}
