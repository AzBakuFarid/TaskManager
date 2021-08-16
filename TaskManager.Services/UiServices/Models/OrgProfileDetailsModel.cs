using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManager.Services.UiServices.Models
{
    public class OrgProfileDetailsModel
    {
        [Display(Name = "Name of organziation")] public string OrgName { get; set; }
        [Display(Name = "Phone number of organziation")] public string OrgPhone { get; set; }
        [Display(Name = "Address of organziation")] public string OrgAddress { get; set; }
        [Display(Name = "Created at")] public string CreatedAt { get; set; }
        public int Id { get; set; } 
        public IEnumerable<OrganizationTaskViewModel> Tasks { get; set; }
    }
    public class OrganizationTaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
