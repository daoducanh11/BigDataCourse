using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
                HttpContext.Session.SetString("_user", JsonSerializer.Serialize(res));
                return RedirectToAction("Index", "Recommend");
            }
            ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
            return View();
        }

        public IActionResult Loguot()
        {
            HttpContext.Session.Remove("_user");
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
