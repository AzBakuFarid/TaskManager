using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Services.UiServices.Models
{
    public class TaskChangeAssigneViewModel
    {
        public int TaskId { get; set; }
        public List<TaskAssignedUsersViewModel> AssignedUsers { get; set; }
        public List<TaskAssignedUsersViewModel> NotAssignedUsers { get; set; }

    }
}
