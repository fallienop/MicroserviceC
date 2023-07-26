using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Model;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response=await _categoryService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) 
        {
            var response = await _categoryService.GetById(id);
            return CreateActionResultInstance(response);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateCategory(CategoryDto category)
        //{
        //    var response = await _categoryService.CreateAsync(category);
        //    return CreateActionResultInstance(response);

        //}

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto category)
        {
            var response = await _categoryService.CreateAsync(category);
            return CreateActionResultInstance(response);
        }
    }
}
