using System;
using System.Collections.Generic;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Domain.Entites
{
    public class Organization : IPrimaryKey<int>, IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
