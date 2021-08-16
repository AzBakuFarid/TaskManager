using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Services.Organizations.Data
{
    public interface IOrganizationCreateData {
        string OrgName { get; set; }
        string OrgPhone { get; set; }
        string OrgAddress { get; set; }
    }
    //public class OrganizationCreateData : IOrganizationCreateData
    //{
    //    public string OrgName { get; set; }
    //    public string OrgPhone { get; set; }
    //    public string OrgAddress { get; set; }
    //}
}
