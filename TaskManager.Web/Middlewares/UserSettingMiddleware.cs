using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Services.Users;

namespace TaskManager.Web.Middlewares
{
    public class UserSettingMiddleware
    {
        private readonly RequestDelegate _next;

        public UserSettingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            { 
                var userService = (IUserService)httpContext.RequestServices.GetService(typeof(IUserService));

                var userId = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
                var user = userService.GetUserForDetails(userId);
                httpContext.Items["User"] = user;
            }
            await _next.Invoke(httpContext);

        }
    }
}
