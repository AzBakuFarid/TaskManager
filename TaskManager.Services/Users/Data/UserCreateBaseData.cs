using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Services.Users.Data
{
    public interface IUserCreateBaseData
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}
