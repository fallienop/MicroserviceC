using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FreeCourse.Shared;
using Microsoft.EntityFrameworkCore.Diagnostics;
using FreeCourse.Shared.Dtos;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using static IdentityServer4.IdentityServerConstants;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FreeCourse.IdentityServer.Controller
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _usermanager;

        public UserController(UserManager<ApplicationUser> usermanager)
        {
            _usermanager = usermanager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto signup)
        {
            var user = new ApplicationUser()
            {

                UserName = signup.UserName,
                Email = signup.Email,
                City = signup.City
            };
            var result = await _usermanager.CreateAsync(user, signup.Password);
            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x=>x.Description).ToList(),404));
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if(userIdClaim== null)
            {
                return BadRequest();
            }
            var user = await _usermanager.FindByIdAsync(userIdClaim.Value);
            if(user==null)
            {
                return BadRequest();
            }
            return Ok(new {Id=user.Id,UserName=user.UserName,Email=user.Email,City=user.City});

        }
    }
}
