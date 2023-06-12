using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MvcWeb.Models
{
    [Index(nameof(Email), IsUnique=true)]
    [Index(nameof(UserName), IsUnique=false)]
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public UserStatus Status { get; set; }
    }
    public enum UserStatus
    {
        Active,
        Blocked
    }
}