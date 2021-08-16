
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Services.Users.Data
{
    public interface IOrdinaryUserCreateData : IUserCreateBaseData
    {
        string Name { get; set; }
        string Surname { get; set; }
        string Username { get; set; } // tapsiriqda bu qeyd olunmayib. amma Identity-de username email value-sunu yazmaq istemedim... neticede real tapsiriq deyil, test-dir.. ona gore bele improvizasiya eledim
    }
    public class OrdinaryUserCreateData : IOrdinaryUserCreateData
    {
        [Required, MaxLength(200)] public string Name { get; set; }
        [Required, MaxLength(200)] public string Surname { get; set; }
        [Required, MaxLength(256)] public string Username { get; set; } 
        [Required, EmailAddress] public string Email { get; set; }
        [MinLength(6), DataType(DataType.Password)] public string Password { get; set; }
        public string OrganizationName { get; set; }
    }

}
