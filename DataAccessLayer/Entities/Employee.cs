using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    [Index(nameof(Name))]
    public class Employee : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        public string? Address { get; set; }
        [Column(TypeName ="money")]
        
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage ="Please Choose a Department")]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
