using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Services.Helpers;

namespace TaskManager.Web.Extensions
{
    public static class SeedExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var repo = services.GetService<RoleManager<IdentityRole>>();
                var config = services.GetService<IConfiguration>();
                var rolenames = config.GetSection("Roles").Get<List<string>>();
                DataSeeder.SeedRoles(repo, rolenames);
            }
            return host;
        }
    }
}
