using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.DataAccessAbstractions;
using TaskManager.Domain.Entites;
using TaskManager.EfCore.Data;
using TaskManager.EfCore.Extensions;

namespace TaskManager.EfCore.Repositories
{
    public class OrganizationRepository : BaseRepository, IOrganizationRepository
    {
        public OrganizationRepository(AppDbContext ctx): base (ctx)
        {
        }
        public Organization FindByName(string name, params string[] relations)
        {
            var model = _dbContext.Organizations.AddInclude(relations).FirstOrDefault(f => f.Name.Equals(name));
            return model;
        }
    }
}
