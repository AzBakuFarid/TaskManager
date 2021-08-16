using System;
using System.Collections.Generic;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Domain.Entites
{
    public class Task : IPrimaryKey<int>, IAuditable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; }
        public TaskStatusEnum Status { get; set; }

        public ICollection<UserTask> UserTasks { get; set; } = new HashSet<UserTask>();

        public virtual User CreatedBy { get; set; }
    }
}
