using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.DataAccessAbstractions;
using TaskManager.Domain.Entites;
using TaskManager.EfCore.Data;
using TaskManager.EfCore.Repositories;

namespace TaskManager.EfCore.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDataAccess(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("TaskManager.Web")
                ))
                .AddScoped<IBaseRepository, BaseRepository>()
                .AddScoped<IOrganizationRepository, OrganizationRepository>()
                .AddScoped<ITaskRepository, TaskRepository>()
                .AddScoped<IUserRepository, UserRepository>();

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;


            }).AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}
