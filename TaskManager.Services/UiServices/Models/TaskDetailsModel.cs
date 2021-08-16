using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManager.Services.UiServices.Models
{
    public class TaskDetailsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Created by")] public string CreatedBy { get; set; }
        [Display(Name = "Created at")] public string CreatedAt { get; set; }
        public string Deadline { get; set; }
        public string Status { get; set; }
        public IEnumerable<TaskAssignedUsersViewModel> AssignedUsers { get; set; }
    }
    public class TaskAssignedUsersViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public bool IsSelected { get; set; }
        [Display(Name = "Assigned by")] public string AssignedBy { get; set; }
        [Display(Name = "Assigned at")] public string AssignedAt { get; set; }
    }
}
