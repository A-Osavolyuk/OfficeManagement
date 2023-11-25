using System.ComponentModel.DataAnnotations;

namespace OfficeManagerMVC.Models.DTOs
{
    public class DepartmentDto
    {
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
