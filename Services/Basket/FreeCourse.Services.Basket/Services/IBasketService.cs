﻿using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userid);
        Task<Response<bool>>SaveOrUpdate(BasketDto basket);
        Task<Response<bool>>Delete(string userid);
    }
}
