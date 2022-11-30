using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Recommender.Parsers;
using Microsoft.AspNetCore.Mvc;

namespace BigDataCourse.Areas.Admin.Controllers
{
    public class RecommendController : Controller
    {
        private readonly IUserActionRepository _userActionRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITagRepository _tagRepository;
        public async Task<IActionResult> Index()
        {
            UserBehaviorDatabaseParser parser = new UserBehaviorDatabaseParser(_userActionRepository, _articleRepository, _userRepository, _tagRepository);
            UserBehaviorDatabase db = await parser.LoadUserBehaviorDatabase();
            return View();
        }
    }
}
