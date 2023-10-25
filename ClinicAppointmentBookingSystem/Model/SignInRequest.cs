using System.ComponentModel.DataAnnotations;

namespace ClinicAppointmentBookingSystem.Model
{
    public class SignInRequest
    {
        [Required, EmailAddress]
        public string? EmailId { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
