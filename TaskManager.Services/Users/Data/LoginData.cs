using System.ComponentModel.DataAnnotations;

namespace TaskManager.Services.Users.Data
{
    public interface ILoginData
    {
        string Username { get; set; }
        string Password { get; set; }
    }
    public class LoginData : ILoginData
    {
        [Required, MaxLength(256)] public string Username { get; set; }
        [MinLength(6), DataType(DataType.Password)] public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
