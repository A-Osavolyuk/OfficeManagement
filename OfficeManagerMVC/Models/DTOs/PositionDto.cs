namespace OfficeManagerMVC.Models.DTOs
{
    public class PositionDto
    {
        public string PositionName { get; set; } = string.Empty;
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
    }
}
