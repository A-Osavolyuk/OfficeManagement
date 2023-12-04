using System.ComponentModel.DataAnnotations;

namespace OfficeManagerMVC.Models.DTOs
{
    public class RegistrationRequestDto
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
