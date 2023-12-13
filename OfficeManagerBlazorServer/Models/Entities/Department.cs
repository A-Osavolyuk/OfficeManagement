using System.ComponentModel.DataAnnotations;

namespace OfficeManagerBlazorServer.Models.Entities
{
    public class Department
    {
        [Display(Name = "Department ID")]
        public int DepartmentId { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
