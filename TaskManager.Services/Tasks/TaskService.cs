using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using TaskManager.Domain.DataAccessAbstractions;
using TaskManager.Domain.Entites;
using TaskManager.Domain.Enums;
using TaskManager.Services.Exceptions;
using TaskManager.Services.HttpRequest;
using TaskManager.Services.Notification;
using TaskManager.Services.Tasks.Data;

namespace TaskManager.Services.Tasks
{
    public interface ITaskService
    {
        Task Create(ITaskCreatedata data);
        void AddUsersToTask(ITaskChangeAssigneData data);
        void AddUserToTask(User user, Task task);
        void RemoveUsersFromTask(ITaskChangeAssigneData data);
        void RemoveUserFromTask(User user, IEnumerable<UserTask> tasks);
        List<Task> List();
        Task FindById(int id);
    }
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;
        private readonly IUserRepository _userManager;
        private readonly IAppEmailService _emailService;
        private readonly IHttpContextService _requestService;
        private User CurrentUser;

        public TaskService(ITaskRepository repo, IUserRepository userManager, 
            IAppEmailService emailService, IHttpContextService requestService)
        {
            _repo = repo;
            _userManager = userManager;
            _emailService = emailService;
            _requestService = requestService;
        }

        public List<Task> List()
        {
            return _repo.ListForOrganization(GetCurrentUser().OrganizationId);
        }

        public Task FindById(int id)
        {
            var task = _repo.FindById<Task, int>(id, "UserTasks.User", "UserTasks.CreatedBy", "CreatedBy") ?? throw new NotFoundException($"task by id {id} is not found");
            
            return  task.CreatedBy.OrganizationId == GetCurrentUser().OrganizationId ? task : throw new OperationNotAllowedException();
        }


        public Task Create([NotNull] ITaskCreatedata data)
        {
            var task = new Task {
                Deadline = data.Deadline.Value, 
                Description = data.Description, 
                Title = data.Title,
                Status = TaskStatusEnum.New,
                //CreatedById = GetCurrentUser().Id
            };
            _repo.Create(task);
            _repo.Commit();
            return task;
        }

        public void AddUsersToTask(ITaskChangeAssigneData data)
        {
            // mence organization daxilindeki her userin tasklari istediyi kimi manage elemek huququ olmamalidi.
            // amma verilen tapsiriqda bunu bagli bir restriction yox idi
            var task = FindById(data.TaskId);
            var users = _userManager.List(data.Users);
            foreach (var user in users)
            {
                AddUserToTask(user, task);
            }
            _repo.Commit();
            _emailService.NotifyMultipleUsersAboutTaskAssignement(users, task);

        }
        public void AddUserToTask(User user, Task task)
        {
            // bax: AddUsersToTask methoduna yaxdigim commente
            if (user != null && task != null)
            {
                CheckIfAssigneChangeAllowed(GetCurrentUser(), task, user);
                var userTask = new UserTask { User = user, Task = task };
                _repo.Create(userTask);
            }
        }
        public void RemoveUsersFromTask(ITaskChangeAssigneData data)
        {
            // bax: AddUsersToTask methoduna yaxdigim commente

            var task = FindById(data.TaskId);
            var users = _userManager.List(data.Users);

            foreach (var user in users)
            {
                RemoveUserFromTask(user, task.UserTasks);
            }
            _repo.Commit();

        }
        public void RemoveUserFromTask(User user, IEnumerable<UserTask> tasks)
        {            
            // bax: AddUsersToTask methoduna yaxdigim commente

            if (user != null && tasks.Count() > 0)
            {
                var userTask = tasks.FirstOrDefault(s => s.UserId.Equals(user.Id));
                if (userTask != null)
                {
                    CheckIfAssigneChangeAllowed(GetCurrentUser(), tasks.First().Task, user);
                    _repo.Delete(userTask);

                }
            }
        }

        private User GetCurrentUser()
        {
            CurrentUser ??= _requestService.GetCurrentUserAndEnsureIsNotNull();
            
            return CurrentUser;
        }

        private void CheckIfAssigneChangeAllowed(User currentUser, Task task, User targetUser)
        {
            // burani custom AuthHandler ile de hell ede bilerdim.. ama bu usul mene daha eleqant geldi
            var taskCreatedUser = task.CreatedBy ?? _userManager.FindById(task.CreatedById);
            string errorMessage;
            // burda kohne switch islede bilmirem, butun compare-lerde istirak eden vahid deyer yoxdu.
            // Positional patterns ile switch daya iyrenc olacaqdi deye istifade elemedim..
            // yazdigim usul daha eleqant geldi mene....
            errorMessage = 
                 currentUser.OrganizationId != taskCreatedUser.OrganizationId ? "You can not manage tasks of other organizations"
                : targetUser.OrganizationId != taskCreatedUser.OrganizationId ? "You can change assigne of tasks only from your organization and only for users of your organization"
                : targetUser.OrganizationId != currentUser.OrganizationId ? "You can change task assigne only for users of your organization"
                //: targetUser.Id == currentUser.Id ? "You can not change task assigne of your own"   texniki tapsiriqda yoxdu deye commente aldim.. amma mence adam oz tasklarini assigne deyismemelidi
                : null;
                
            if (string.IsNullOrEmpty(errorMessage))
            {
                return;
            }

            throw new OperationNotAllowedException(errorMessage);
        }
    }
}
