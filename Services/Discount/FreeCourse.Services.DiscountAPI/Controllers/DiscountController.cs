using FreeCourse.Services.DiscountAPI.Model;
using FreeCourse.Services.DiscountAPI.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.DiscountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : CustomBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resp = await _discountService.GetAll();
            return CreateActionResultInstance(resp);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var resp=await _discountService.GetById(id);
            return CreateActionResultInstance(resp);
        }
        [HttpGet]
        [Route("/api/[controller]/[action]/{code}")]
        public async Task<IActionResult> GetbyUserIdandCode(string code)
        {
            var userid = _sharedIdentityService.GetUserId;
            var resp=await _discountService.GetByCodeandUserId(userid, code);
            return CreateActionResultInstance(resp);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var resp=await _discountService.DeleteById(id);
            return CreateActionResultInstance(resp);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Discount disc)
        {
            var resp = await _discountService.Save(disc);
            return CreateActionResultInstance(resp);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Discount disc)
        {
            var resp=await _discountService.Update(disc);
            return CreateActionResultInstance(resp);
        }
    }
}
