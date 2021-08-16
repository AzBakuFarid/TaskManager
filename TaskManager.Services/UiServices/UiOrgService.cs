using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Entites;
using TaskManager.Services.UiServices.Models;

namespace TaskManager.Services.UiServices
{
    public class UiOrgService
    {
        public OrgProfileDetailsModel GetOrgProfileViewModel(Organization org)
        {
            var viewModel = new OrgProfileDetailsModel { Id = org.Id, OrgAddress = org.Address, OrgName = org.Name, OrgPhone = org.PhoneNumber, CreatedAt = org.CreatedAt.ToString("dd.MM.yyyy HH:mm") };
            var tasks = org.Users.SelectMany(s => s.UserTasks).Select(s => s.Task);
            viewModel.Tasks = tasks.Select(s => new OrganizationTaskViewModel { Id = s.Id, Title = s.Title });
            return viewModel;
        }
    }
}
