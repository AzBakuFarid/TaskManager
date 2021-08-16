using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskManager.Services.Organizations.Data;

namespace TaskManager.Services.Users.Data
{
    public interface IOrgProfileCreateData : IOrganizationCreateData, IUserCreateBaseData
    {
        string Username { get; set; }
    }
    public class OrgProfileCreateData : IOrgProfileCreateData
    {
        [Display(Name = "Username"), Required, MaxLength(256)]  public string Username { get; set; }
        [Display(Name = "Organization's name"), Required, MaxLength(200)] public string OrgName { get; set; }
        [Display(Name = "Organization's phone number"), MaxLength(20)] public string OrgPhone { get; set; }
        [Display(Name = "Organization's address"), MaxLength(255)] public string OrgAddress { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required, MinLength(8), DataType(DataType.Password)] public string Password { get; set; }
        [Compare("Password"), DataType(DataType.Password), Display(Name = "Confirm password")] public string ConfirmPassword { get; set; }
    }

}
