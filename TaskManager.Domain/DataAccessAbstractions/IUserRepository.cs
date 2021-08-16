using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entites;

namespace TaskManager.Domain.DataAccessAbstractions
{
    public interface IUserRepository
    {
        IdentityResult CreateUser(User user, string password);
        IdentityResult AddToRole(User user, string roleName);
        User FindById(string id, params string[] relations);
        List<User> ListUsers(int orgId, params string[] relations);
        List<User> List(IEnumerable<string> idList);
        User FindByUsername(string username, params string[] relations);
        bool CheckPassword(User user, string password);
    }
}
