using Dapper;
using Dapper.Contrib.Extensions;
using FreeCourse.Services.DiscountAPI.Model;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;

namespace FreeCourse.Services.DiscountAPI.Services
{
    public class DiscountService : IDiscountService
    {

        private readonly IConfiguration _configuration;

        private readonly IDbConnection _dbConnection;
        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> DeleteById(int id)
        {
            var deletediscount= await _dbConnection.ExecuteAsync($"delete from discount where id={id}");
            if (deletediscount > 0)
            {
                return Response<NoContent>.Success(200);
            }
            else
            {
                return Response<NoContent>.Fail("Could not be deleted", 404);
            }

            
        }


        public async Task<Response<List<Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Discount>("select * from discount");
            return Response<List<Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Discount>> GetByCodeandUserId(string code, string userId)
        {
            var getbyid = (await _dbConnection.QueryAsync<Discount>($"select * from discount where userid={userId} and code={code}")).FirstOrDefault();
            if (getbyid != null)
            {
                return Response<Discount>.Success(200);
            }
            else
            {
                return Response<Discount>.Fail("could not be found", 404);
            }

        }

        public async Task<Response<Discount>> GetById(int id)
        {
            var discountbyid = (await _dbConnection.QueryAsync<Discount>($"select * from discount where id={id}")).SingleOrDefault();
            if (discountbyid == null)
            {
          
                return Response<Discount>.Fail("not found", 404);

            }
            return Response<Discount>.Success(discountbyid, 200);
        }

        public async Task<Response<NoContent>> Save(Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync($"insert into discount(userid,rate,code) values('{discount.UserId}',{discount.Rate},'{discount.Code}')");
            if (status > 0)
            {
                return Response<NoContent>.Success(200);
            }
            else
            {
                return Response<NoContent>.Fail("not saved", 500);
            }
        }

        public async Task<Response<NoContent>> Update(Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync($"update discount set userid=@UserId,rate=@Rate,code=@Code where id=@Id",discount);
            if (status > 0)
            {
                return Response<NoContent>.Success(204);

            }
            else
            {
                var chk = await _dbConnection.ExecuteAsync($"SELECT COUNT(*) FROM discounts WHERE id = {discount.Id}");
                if (chk == 1)
                {
                    return Response<NoContent>.Fail("Could not be updated", 500);
                }
                else { 
                    return Response<NoContent>.Fail("Could not found", 404);
                }
            }
        }
    }
}
