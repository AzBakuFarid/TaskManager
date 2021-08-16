using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Services.Organizations;
using TaskManager.Services.UiServices;

namespace TaskManager.Web.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _orgService;
        private readonly UiOrgService _uiService;


        public OrganizationController(IOrganizationService orgService, UiOrgService uiService)
        {
            _orgService = orgService;
            _uiService = uiService;
        }
        public IActionResult OrgProfile(int id)
        {
            var org = _orgService.GetOrganizationProfile(id);
            var viewModel = _uiService.GetOrgProfileViewModel(org);
            return View(viewModel);
        }
    }
}
