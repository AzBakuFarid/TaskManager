using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TaskManager.Services.Exceptions;
using TaskManager.Services.Tasks;
using TaskManager.Services.Tasks.Data;
using TaskManager.Services.UiServices;
using TaskManager.Services.UiServices.Models;
using TaskManager.Services.Users;

namespace TaskManager.Web.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly UiTaskService _uiService;

        public TaskController(ITaskService taskService, UiTaskService uiService, IUserService userService)
        {
            _taskService = taskService;
            this._uiService = uiService;
            _userService = userService;
        }

        public IActionResult List()
        {
            var tasks = _taskService.List();
            var model = _uiService.GetTaskListViewModels(tasks);
            return View(model);
        }
        public IActionResult Details(int id)
        {
            var task = _taskService.FindById(id);
            var model = _uiService.GetTaskDetailsModel(task);
            return View(model);
        }
        [HttpGet]
        public IActionResult ChangeAssigne(int id)
        {
            var task = _taskService.FindById(id);
            var usersNotInTask = _userService.ListUsersNotAssignedToTask(task);
            var model = _uiService.GetChangeAssigneModel(id, task.UserTasks.Select(s => s.User), usersNotInTask);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddUser(TaskChangeAssigneViewModel request)
        {
            var data = new TaskChangeAssigneData { TaskId = request.TaskId, Users = request.NotAssignedUsers.Where(w => w.IsSelected).Select(s => s.Id) };
            _taskService.AddUsersToTask(data);
            return RedirectToAction("ChangeAssigne", new { id = request.TaskId});
        }
        [HttpPost]
        public IActionResult RemoveUser(TaskChangeAssigneViewModel request)
        {
            var data = new TaskChangeAssigneData { TaskId = request.TaskId, Users = request.AssignedUsers.Where(w => w.IsSelected).Select(s => s.Id) };
            _taskService.RemoveUsersFromTask(data);
            return RedirectToAction("ChangeAssigne", new { id = request.TaskId });

        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = _uiService.GetTaskCreateModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(TaskCreateModel request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var task = _taskService.Create(request);
                    return RedirectToAction("Details", new { id = task.Id });
                }
                catch (BadRequestException ex)
                {
                    ModelState.AddModelError(ex.FieldName, ex.Message);
                }

            }
            return View(request);
        }


    }
}
