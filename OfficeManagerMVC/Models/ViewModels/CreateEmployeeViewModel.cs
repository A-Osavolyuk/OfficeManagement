using OfficeManagerMVC.Models.Entities;

namespace OfficeManagerMVC.Models.ViewModels
{
    public class CreateEmployeeViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
        public DateTime DateOfHire { get; set; } = DateTime.UtcNow;
        public int PositionId { get; set; }

        public List<Position> Positions { get; set; } = new List<Position>();
    }
}
