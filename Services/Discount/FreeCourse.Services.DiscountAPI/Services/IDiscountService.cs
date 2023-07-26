using FreeCourse.Services.DiscountAPI.Model;
using FreeCourse.Shared.Dtos;
using System.Globalization;

namespace FreeCourse.Services.DiscountAPI.Services
{
    public interface IDiscountService
    {
        Task<Response<List<Discount>>> GetAll();
        Task<Response<Discount>> GetById(int id); 
        Task<Response<NoContent>> Save(Discount discount);
        Task<Response<NoContent>> DeleteById(int id);
        Task<Response<NoContent>> Update(Discount discount);

        Task<Response<Discount>> GetByCodeandUserId(string code,string userId); 

    }
}
