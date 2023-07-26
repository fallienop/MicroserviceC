using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;
using System.Text.Json;

namespace FreeCourse.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
    private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> Delete(string userid)
        {
             var status =await _redisService.GetDb(1).KeyDeleteAsync(userid);
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("cannot delete", 404);

        }

        public async Task<Response<BasketDto>> GetBasket(string userid)
        {

            var existbasket = await _redisService.GetDb(1).StringGetAsync(userid);
           if(String.IsNullOrEmpty(existbasket))
            {
                return Response<BasketDto>.Fail("basket not found", 404);
            }
            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existbasket),200);
        }


        public async Task<Response<bool>> SaveOrUpdate(BasketDto basket)
        {
            var status = await _redisService.GetDb(1).StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("cannot save", 500);
        }
    }
}
