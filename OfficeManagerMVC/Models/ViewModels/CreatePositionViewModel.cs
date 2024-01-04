using OfficeManagerMVC.Models.Entities;

namespace OfficeManagerMVC.Models.ViewModels
{
    public class CreatePositionViewModel
    {
        public string PositionName { get; set; } = string.Empty;
        public int DepartmentId { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();
    }
}
