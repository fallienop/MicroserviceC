using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Net.WebRequestMethods;

namespace FreeCourse.Shared.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        private IHttpContextAccessor _contextAccessor;

        public SharedIdentityService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

      //  public string GetUserId => _contextAccessor.HttpContext.User.FindFirst("sub").Value;    
        public string GetUserId
        {
            get
            {
                var user = _contextAccessor.HttpContext?.User;
                if (user != null)
                {
                    var userIdClaim = user.FindFirst("sub");
                    if (userIdClaim != null)
                    {
                        return userIdClaim.Value;
                    }
                }

                return null;
            }
        }
     





    }
}
