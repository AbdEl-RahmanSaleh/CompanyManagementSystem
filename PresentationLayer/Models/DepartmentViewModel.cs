using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code Is Required")]
        public string? Code { get; set; }
        [Required(ErrorMessage = "Name Is Requiered")]
        [MaxLength(50)]
        public string? Name { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
    }
}
