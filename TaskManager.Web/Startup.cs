using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.EfCore.Extensions;
using TaskManager.Services.Extensions;
using TaskManager.Web.Extensions;
using TaskManager.Web.Filters;

namespace TaskManager.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(UiExceptionFilter));
            });
            services.RegisterDataAccess(Configuration);
            services.RegisterServices(Configuration);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Can create ordinary user",
                    // duzdu serte yazilmisdi ki ancaq adminler user yarada biler.. 
                    // amma ola biler ki, admin olmayan, lakin user yaratmaq icazesi olan istifadeci de olsun
                    // hem de bununla claim de bilirem demek istedim ))
                    policy => policy.RequireAssertion(context => context.User.HasClaim(c => c.Type.Equals("Can create ordinary user", StringComparison.OrdinalIgnoreCase) && c.Value.Equals("Can create ordinary user", StringComparison.OrdinalIgnoreCase)) || context.User.IsInRole("Admin"))
                    );
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/user/accessdenied";
                options.LoginPath = "/user/login";
                options.LogoutPath = "/user/logout";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication().AddUserSettingMiddleware();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
