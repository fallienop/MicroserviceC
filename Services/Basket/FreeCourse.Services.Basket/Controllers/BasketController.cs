    using FreeCourse.Services.Basket.Dtos;
    using FreeCourse.Services.Basket.Services;
    using FreeCourse.Shared.ControllerBases;
    using FreeCourse.Shared.Dtos;
    using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

    namespace FreeCourse.Services.Basket.Controllers
    {

 //   [Authorize]
    [Route("api/[controller]")]
        [ApiController]
        public class BasketController : CustomBaseController
        {

            private readonly IBasketService _basketService;
            private readonly ISharedIdentityService _sharedidentity;

            public BasketController(IBasketService basketService, ISharedIdentityService sharedidentity)
            {
                _basketService = basketService;
                _sharedidentity = sharedidentity;
            }

            [HttpGet]
            public async Task<IActionResult> GetBasket()
            {
                string id = _sharedidentity.GetUserId;
              var res= await _basketService.GetBasket(id);
         
                    return CreateActionResultInstance(res);
            

            }

            [HttpPost]
            public async Task<IActionResult> basketsaveupdate(BasketDto basket)
            {
                var res=await _basketService.SaveOrUpdate(basket);
                return CreateActionResultInstance(res);
            }

            [HttpDelete]
            public async Task<IActionResult> Delete()
            {
                return CreateActionResultInstance(await _basketService.Delete(_sharedidentity.GetUserId));
            }
        }
    }
