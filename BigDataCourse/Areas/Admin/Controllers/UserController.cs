using BigDataCourse.Areas.Admin.Authorization;
using BigDataCourse.Areas.Admin.Data;
using BigDataCourse.Areas.Admin.Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BigDataCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizationAdmin]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserActionRepository _userActionRepository;
        public UserController(IUserRepository userRepository, IUserActionRepository userActionRepository)
        {
            _userRepository = userRepository;
            _userActionRepository = userActionRepository;
        }

        public async Task<IActionResult> Index(string name = "", string phoneNumber = "", int page = 1, int pageSize = 10)
        {
            ViewBag.pageOffset = (page - 1) * pageSize;
            if (name == null)
                name = "";
            if (phoneNumber == null)
                phoneNumber = "";
            ViewBag.name = name;
            ViewBag.phoneNumber = phoneNumber;
            return View(await _userRepository.Get(name, phoneNumber, page, pageSize));
        }

        public async Task<IActionResult> ActionHistory(int id, string uAction = "", int page = 1, int pageSize = 20)
        {
            ViewBag.pageOffset = (page - 1) * pageSize;
            ViewBag.uAction = uAction;
            return View(await _userActionRepository.GetByUserID(id, uAction, page, pageSize));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _userRepository.Delete(id);
            return RedirectToAction("Index", "User");
        }
    }
}
