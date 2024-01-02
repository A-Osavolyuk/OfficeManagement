namespace OfficeManagerMVC.Models.Entities
{
    public class Position
    {
        public string PositionName { get; set; } = string.Empty;
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
    }
}
