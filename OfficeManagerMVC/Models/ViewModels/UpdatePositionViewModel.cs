using OfficeManagerMVC.Models.Entities;

namespace OfficeManagerMVC.Models.ViewModels
{
    public class UpdatePositionViewModel
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; } = string.Empty;
        public int DepartmentId { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();
    }
}
