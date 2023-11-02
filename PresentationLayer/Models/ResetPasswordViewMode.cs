using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class ResetPasswordViewMode
    {
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 charachters Containing Numbers, UpperCase and LowerCase Charachters and at least one NonAlphaNumeric charchter like' @,#,$' ")]
        public string? Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password Mismatch")]
        public string? ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
