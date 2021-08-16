using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using TaskManager.Domain.DataAccessAbstractions;
using TaskManager.Domain.Entites;
using TaskManager.Services.Exceptions;
using TaskManager.Services.Organizations.Data;

namespace TaskManager.Services.Organizations
{
    public interface IOrganizationService {
        Organization Create(IOrganizationCreateData data);
        Organization GetOrganizationProfile(int id);
    }
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _repo;

        public OrganizationService(IOrganizationRepository repo)
        {
            _repo = repo;
        }

        public Organization Create([NotNull] IOrganizationCreateData data)
        {
            var organization = _repo.FindByName(data.OrgName);
            if (organization != null)
            {
                throw new BadRequestException(nameof(data.OrgName), $"Organization with name {data.OrgName} already exists. Choose another one");
            }
            var newOrganization = new Organization { Address = data.OrgAddress, Name = data.OrgName, PhoneNumber = data.OrgPhone };
            _repo.Create(newOrganization);
            return newOrganization;
        }
        public Organization GetOrganizationProfile(int id)
        {
            var org = _repo.FindById<Organization, int>(id, "Users.UserTasks.Task");
            return org;
        }
    }
}
