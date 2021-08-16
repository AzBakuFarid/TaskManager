using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManager.Services.UiServices.Models
{
    public class UserDetailsModel
    {
         public string Id { get; set; }
         public string Name { get; set; }
         public string Surname { get; set; }
         public string Username { get; set; }
        public string Email { get; set; }
        public string OrganizationName { get; set; }
        public IEnumerable<UserTaskViewModel> Tasks { get; set; }

    }
    public class UserTaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "Assigned at")] public string AssignedAt { get; set; }
        [Display(Name = "Assigned by")] public string AssignedBy { get; set; }
    }
}
