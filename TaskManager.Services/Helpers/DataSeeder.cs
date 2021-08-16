using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.Services.Helpers
{
    public class DataSeeder
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager, List<string> roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    var result = roleManager.CreateAsync(new IdentityRole(roleName)).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception("Something went wrong during creation of roles. Error is as below" + 
                                            Environment.NewLine +  
                                            string.Join(Environment.NewLine,result.Errors.Select(s => s.Description))
                                            );
                    }
                }
            }

        }
    }
}
