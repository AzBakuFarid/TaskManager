using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Domain.Entites
{
    public class User : IdentityUser, IAuditable
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual User CreatedBy { get; set; }

        public ICollection<UserTask> UserTasks { get; set; } = new HashSet<UserTask>();
        public ICollection<UserTask> TasksAssignedByMe { get; set; } = new HashSet<UserTask>();
        public ICollection<Task> CreatedTasks { get; set; } = new HashSet<Task>();
        public ICollection<User> CreatedUsers { get; set; } = new HashSet<User>();

    }
}
