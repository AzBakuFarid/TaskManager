using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Web.Middlewares;

namespace TaskManager.Web.Extensions
{
    public static  class MIddlewareExtensions
    {
        public static IApplicationBuilder AddUserSettingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<UserSettingMiddleware>();

            return app;
        }
    }
}
