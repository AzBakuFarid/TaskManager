using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Domain.Entites
{
    public class UserTask : IAuditable
    {
        public string UserId { get; set; } 
        public virtual User User { get; set; }

        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User CreatedBy { get; set; }

    }
}
