using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        public string? Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 charachters Containing Numbers, UpperCase and LowerCase Charachters and at least one NonAlphaNumeric charchter like' @,#,$' ")]
        public string? Password { get; set; }
        [Required]
        [Compare(nameof(Password) , ErrorMessage = "Password Mismatch")]
        public string? ConfirmPassword { get; set; }
        [Required]
        public bool IsAgree{ get; set; }


    }
}
