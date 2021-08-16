using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entites;

namespace TaskManager.EfCore.Data
{
    public partial class AppDbContext : IdentityDbContext<User>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> opt, IHttpContextAccessor httpContextAccessor) : base(opt)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserTask>(ut =>
            {
                ut.HasKey(p => new { p.UserId, p.TaskId });
                ut.HasOne(p => p.Task).WithMany(p => p.UserTasks).HasForeignKey(p => p.TaskId);
                ut.HasOne(p => p.User).WithMany(p => p.UserTasks).HasForeignKey(p => p.UserId);
                ut.HasOne(p => p.CreatedBy).WithMany(p => p.TasksAssignedByMe).HasForeignKey(p => p.CreatedById).OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<Organization>(t =>
            {
                t.Property(p => p.Name).IsRequired().HasMaxLength(200);
                t.Property(p => p.PhoneNumber).HasMaxLength(20);
                t.Property(p => p.Address).HasMaxLength(255);
            });

            builder.Entity<User>(u =>
            {
                // eslinde default olaraq property adlandirmallarina gore buna ehtiyac yoxdu.. 
                // amma men hemis bunu yaziram... bura baxanda her sey anlasilan olsun deye
                u.HasOne(f => f.Organization).WithMany(fk => fk.Users).HasForeignKey(f => f.OrganizationId);
                u.HasOne(p => p.CreatedBy).WithMany(p => p.CreatedUsers).HasForeignKey(p => p.CreatedById).OnDelete(DeleteBehavior.ClientSetNull);

                //  istifade etmeyeceyimiz columnlari yigisdiraq
                u.Ignore(i => i.AccessFailedCount)
                .Ignore(i => i.EmailConfirmed)
                .Ignore(i => i.LockoutEnabled)
                .Ignore(i => i.LockoutEnd)
                .Ignore(i => i.PhoneNumberConfirmed)
                .Ignore(i => i.TwoFactorEnabled);

                u.Property(p => p.Name).HasMaxLength(200);
                u.Property(p => p.Surname).HasMaxLength(200);
            });

            builder.Entity<Task>(t =>
            {
                t.HasOne(f => f.CreatedBy).WithMany(fk => fk.CreatedTasks).HasForeignKey(f => f.CreatedById).OnDelete(DeleteBehavior.ClientSetNull);
                t.Property(p => p.Title).IsRequired().HasMaxLength(1000);
                t.Property(p => p.Description).HasMaxLength(2000);
            });

        }

    }
}
