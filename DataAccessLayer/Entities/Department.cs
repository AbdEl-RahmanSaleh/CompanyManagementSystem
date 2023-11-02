using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Department : BaseEntity
    {
        [Required(ErrorMessage ="Code Is Required")]
        public string? Code { get; set; }
        [Required(ErrorMessage ="Name Is Requiered")]
        [MaxLength(50)]
        public string? Name { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public ICollection<Employee>? Employees { get; set; }
        
    }
}
