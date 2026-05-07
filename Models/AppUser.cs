using System.ComponentModel.DataAnnotations;
namespace SRegister.Models
{
    public class AppUser
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; }  = string.Empty;
    }
}