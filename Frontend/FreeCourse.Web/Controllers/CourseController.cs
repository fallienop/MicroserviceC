using FreeCourse.Shared.Services;
using FreeCourse.Web.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICatalogService _catalogservice;
        private readonly ISharedIdentityService _sharedservice;

        public CourseController(ICatalogService catalogservice, ISharedIdentityService sharedservice)
        {
            _catalogservice = catalogservice;
            _sharedservice = sharedservice;
        }

        public async Task< IActionResult> Index()
        {
            return View(await _catalogservice.GetCourseByUserIdAsync(_sharedservice.GetUserId));  
        }

    }
}
