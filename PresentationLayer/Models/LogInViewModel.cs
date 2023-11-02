using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class LogInViewModel 
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 charachters")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
