using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.DataAccessAbstractions;
using TaskManager.Domain.Entites;
using TaskManager.EfCore.Extensions;

namespace TaskManager.EfCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }
        public IdentityResult CreateUser(User user, string password)
        {
            return _userManager.CreateAsync(user, password).Result;
        }

        public List<User> List(IEnumerable<string> idList)
        {
            return _userManager.Users.Where(w => idList.Contains(w.Id)).ToList();
        }

        public User FindById(string id, params string[] relations)
        {
            return _userManager.Users.AddInclude(relations).FirstOrDefault(f => f.Id.Equals(id));
        }
        public User FindByUsername(string username, params string[] relations)
        {
            return _userManager.Users.AddInclude(relations).FirstOrDefault(f => f.NormalizedUserName.Equals(username.ToUpper()));
        }
        public List<User> ListUsers(int orgId, params string[] relations)
        {
            return _userManager.Users.Where(w => w.OrganizationId == orgId).AddInclude(relations).ToList();
        }
        public bool CheckPassword(User user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password).Result;
        }
        public IdentityResult AddToRole(User user, string roleName)
        {
            return _userManager.AddToRoleAsync(user, roleName).Result;
        }
    }
}
