using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Services.UiServices.Models
{
    public class TaskListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Deadline { get; set; }
        public string Status { get; set; }
        public int AssignedUserCount { get; set; }
        [Display(Name = "Created by")] public string CreatedBy { get; set; }
        [Display(Name = "Created at")] public string CreatedAt { get; set; }
    }
}
