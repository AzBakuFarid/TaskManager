using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TaskManager.Domain.Entites;
using TaskManager.Services.Tasks.Data;
using TaskManager.Services.UiServices.Models;

namespace TaskManager.Services.UiServices
{
    public class UiTaskService
    {
        public List<TaskListViewModel> GetTaskListViewModels([NotNull] IEnumerable<Task> tasks)
        {
            return tasks.Select(s => new TaskListViewModel
            {
                Deadline = s.Deadline.ToString("dd.MM.yyyy HH:mm"),
                Description = s.Description,
                Status = s.Status.ToString(),
                Id = s.Id,
                Title = s.Title,
                AssignedUserCount = s.UserTasks.Count(),
                CreatedBy = s.CreatedBy?.UserName ?? "user deleted",
                CreatedAt = s.CreatedAt.ToString("dd.MM.yyyy HH:mm"),
            }).ToList();
        }
        public TaskDetailsModel GetTaskDetailsModel([NotNull] Task task)
        {
            return new TaskDetailsModel {
                Deadline = task.Deadline.ToString("dd.MM.yyyy HH:mm"),
                Description = task.Description,
                Status = task.Status.ToString(),
                Id = task.Id,
                Title = task.Title,
                CreatedBy = task.CreatedBy?.UserName ?? "user deleted",
                CreatedAt = task.CreatedAt.ToString("dd.MM.yyyy HH:mm"),
                AssignedUsers = task.UserTasks.Select(s => s.User)
                .Select(s => new TaskAssignedUsersViewModel { 
                    Id = s.Id, 
                    Username = s.UserName,
                     AssignedBy = task.UserTasks.FirstOrDefault(f => f.UserId.Equals(s.Id))?.CreatedBy.UserName,
                     AssignedAt = task.UserTasks.FirstOrDefault(f => f.UserId.Equals(s.Id))?.CreatedAt.ToString("dd.MM.yyyy HH:mm")
                })
            };
        }
        public TaskChangeAssigneViewModel GetChangeAssigneModel(int taskId, IEnumerable<User> usersInTask, IEnumerable<User> usersNotInTask)
        {
            return new TaskChangeAssigneViewModel
            { 
                TaskId = taskId, 
                 AssignedUsers = usersInTask.Select(s => new TaskAssignedUsersViewModel { Id = s.Id, Username = s.UserName }).ToList(), 
                 NotAssignedUsers = usersNotInTask.Select(s => new TaskAssignedUsersViewModel { Id = s.Id, Username = s.UserName }).ToList() 
            };
        }
        public TaskCreateModel GetTaskCreateModel()
        {
            return new TaskCreateModel();
        }
    }
}
