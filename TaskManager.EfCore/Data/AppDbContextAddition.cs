using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using TaskManager.Domain.Interfaces;

namespace TaskManager.EfCore.Data
{
    public partial class AppDbContext
    {
        public override int SaveChanges()
        {
            PreSavingActions();
            return base.SaveChanges();
        }
        private void PreSavingActions()
        {
            var entries = ChangeTracker.Entries();
            var now = DateTime.Now;
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            foreach (var entry in entries)
            {
                if (entry.Entity is IAuditable model)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            model.CreatedAt = now;
                            model.CreatedById ??= userId;
                            break;
                    }
                }
            }
        }
    }
}
