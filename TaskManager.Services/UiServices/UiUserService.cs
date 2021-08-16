using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Entites;
using TaskManager.Services.UiServices.Models;
using TaskManager.Services.Users.Data;

namespace TaskManager.Services.UiServices
{
    public class UiUserService
    {
        public OrgProfileCreateData GetOrgProfileCreateModel()
        {
            return new OrgProfileCreateData();
        }
        public OrdinaryUserCreateData GetOrdinaryUserCreateModel()
        {
            return new OrdinaryUserCreateData();
        }
        public UserDetailsModel GetUserDetailsModel(User user)
        {
            return new UserDetailsModel {
                Email = user.Email,
                Name = user.Name,
                OrganizationName = user.Organization.Name,
                Surname = user.Surname,
                Username = user.UserName, 
                Id = user.Id,
                Tasks = user.UserTasks?
                    .Select(s => new UserTaskViewModel { 
                        Id = s.TaskId, 
                        Title = s.Task.Title,
                         AssignedAt = s.CreatedAt.ToString("dd.MM.yyyy HH:mm"),
                         AssignedBy = s.CreatedBy?.UserName ?? "user deleted"
                    }) ?? new List<UserTaskViewModel>()
            };
        }
    }
}
