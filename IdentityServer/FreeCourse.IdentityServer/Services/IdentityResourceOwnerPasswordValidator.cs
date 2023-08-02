using FreeCourse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existuser = await _userManager.FindByNameAsync(context.UserName);
            if (existuser ==null)
            {
                var errors=new Dictionary<string, object>();
                errors.Add("errors", new List<string>() { "Email is incorrect" });
                context.Result.CustomResponse=errors;
                return;
            }
            var passwordcheck=await _userManager.CheckPasswordAsync(existuser, context.Password);
                
            if(passwordcheck==false)
            {
                    var errors=new Dictionary<string, object>();
                errors.Add("errors", new List<string>() { "password is incorrect" });
                context.Result.CustomResponse=errors;
                return;
            }
            context.Result = new GrantValidationResult(existuser.Id.ToString(),OidcConstants.AuthenticationMethods.Password);
        }
    }
}
