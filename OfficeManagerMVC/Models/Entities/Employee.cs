using System.ComponentModel.DataAnnotations;

namespace OfficeManagerMVC.Models.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
        public DateTime DateOfHire { get; set; } = DateTime.UtcNow;
        public int PositionId { get; set; }
    }
}
