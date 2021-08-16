using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.DataAccessAbstractions;
using TaskManager.Domain.Entites;
using TaskManager.Services.Exceptions;
using TaskManager.Services.Helpers;
using TaskManager.Services.HttpRequest;
using TaskManager.Services.Organizations;
using TaskManager.Services.Users.Data;

namespace TaskManager.Services.Users
{
    public interface IUserService {
        User CreateOrgProfile(IOrgProfileCreateData data);
        User CreateOrdinaryUser(IOrdinaryUserCreateData data);
        User GetUserForLogin(LoginData data);
        User GetUserForDetails(string id);
        IEnumerable<User> ListUsersNotAssignedToTask(Task task);

    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userManager;
        private readonly IOrganizationService _orgManager;
        private readonly IHttpContextService _requestService;

        private User CurrentUser;

        public UserService(IUserRepository userManager, IOrganizationService orgManager, IHttpContextService requestService)
        {
            _requestService = requestService;
            _userManager = userManager;
            _orgManager = orgManager;
        }

        public User CreateOrgProfile(IOrgProfileCreateData data)
        {
            var organization = _orgManager.Create(data);
            var user = new User { Email = data.Email, UserName = data.Username, Organization = organization };
            ExecuteIdentityRelatedActions(() => _userManager.CreateUser(user, data.Password));
            ExecuteIdentityRelatedActions(() => _userManager.AddToRole(user, "Admin"));

            return user;
        }

        public User GetUserForDetails(string id)
        {
            return _userManager.FindById(id, "Organization", "UserTasks.Task", "UserTasks.CreatedBy") ?? throw new NotFoundException($"User by id {id} is not found"); 
        }

        public User CreateOrdinaryUser(IOrdinaryUserCreateData data)
        {
            var organization = GetCurrentUser().Organization;
            var user = new User { Email = data.Email, UserName = data.Username, Organization = organization, Name = data.Name, Surname = data.Surname };
            var password = data.Password ?? PasswordGenerator.GetDefaultPassword();
            ExecuteIdentityRelatedActions(() => _userManager.CreateUser(user, password));

            return user;
        }

        public User GetUserForLogin(LoginData data)
        {
            var user = _userManager.FindByUsername(data.Username) ?? throw new BadRequestException(nameof(data.Username), $"User with username {data.Username} does not exists");
            if (!_userManager.CheckPassword(user, data.Password))
            {
                throw new BadRequestException(nameof(data.Password), "Password is incorrect");
            }
            return user;
        }
        public IEnumerable<User> ListUsersNotAssignedToTask(Task task)
        {
            var taskCreatedBy = task.CreatedBy ?? _userManager.FindById(task.CreatedById, "Organization");
            return _userManager.ListUsers(taskCreatedBy.OrganizationId)
                .Where(w => w.UserTasks.Count == 0 || !w.UserTasks.Select(s => s.TaskId).Contains(task.Id));
        }

        #region private_methods
        private User GetCurrentUser()
        {
            CurrentUser ??= _requestService.GetCurrentUserAndEnsureIsNotNull();

            return CurrentUser;
        }
        private void ExecuteIdentityRelatedActions(Func<IdentityResult> action)
        {
            var result = action();
            if (!result.Succeeded)
            {
                throw new BadRequestException(string.Empty, string.Join(Environment.NewLine, result.Errors.Select(s => s.Description)));
            }
        }
        #endregion
    }
}
