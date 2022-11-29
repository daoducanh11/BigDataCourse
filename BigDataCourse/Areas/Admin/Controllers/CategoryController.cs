using BigDataCourse.Areas.Admin.Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BigDataCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index(int? page, int? pageSize)
        {
            return View(await _categoryRepository.Get(page ?? 1, pageSize ?? 10));
        }
    }
}
