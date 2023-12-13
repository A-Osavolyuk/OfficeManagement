using System.ComponentModel.DataAnnotations;

namespace OfficeManagerBlazorServer.Models.DTOs
{
    public class DepartmentDto
    {
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
