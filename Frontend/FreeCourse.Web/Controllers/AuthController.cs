    using FreeCourse.Web.Models;
    using FreeCourse.Web.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
    using System.Runtime.CompilerServices;

    namespace FreeCourse.Web.Controllers
    {
        public class AuthController : Controller
        {
            private readonly IIdentityService _identityservice;

            public AuthController(IIdentityService identityservice)
            {
                _identityservice = identityservice;
            }

            public IActionResult SignIn()
            {
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> SignIn(SigninInput signininput)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
              var response=await _identityservice.SignIn(signininput);
                if (!response.IsSuccessfull)
                {
                    response.Errors.ForEach(x =>
                    {
                        ModelState.AddModelError(string.Empty, x);

                    });
                    return View();
                }
         
                 return   RedirectToAction ("Index", "Home");
            }
          
            public async Task<IActionResult> Logout()
            {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _identityservice.RevokeRefreshToken();
            return RedirectToAction(nameof(HomeController.Index), "Home");
            }

        }
    }
