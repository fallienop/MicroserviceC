using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Operations;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : CustomBaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var response = await _courseService.GetByID(id);

            return CreateActionResultInstance(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response= await _courseService.GetAllAsync();
            return CreateActionResultInstance(response);    
        }
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteCourse(string id)
        {
            var response=await _courseService.CourseDeleteAsync(id);
            return CreateActionResultInstance(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCourse(CourseUpdateDto course)
        {
            var response=await _courseService.CourseUpdateAsync(course);
            return CreateActionResultInstance(response);
        }

        [HttpGet("/api/[controller]/GetByUserId/{userid}")]
        public async Task<IActionResult>GetByUserID(string userid)
        {
            var response = await _courseService.GetAllByUserId(userid);
            return CreateActionResultInstance(response);    
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseCreateDto course)
        {
            var response = await _courseService.CourseCreateAsync(course); 
            return CreateActionResultInstance(response);        
        }

    }
}
