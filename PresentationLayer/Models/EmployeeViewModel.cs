using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    [Index(nameof(Name))]
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        public string? Address { get; set; }
        [Column(TypeName = "money")]

        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        public IFormFile Image { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Please Choose a Department")]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

    }
}
