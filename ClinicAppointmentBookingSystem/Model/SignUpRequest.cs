using System.ComponentModel.DataAnnotations;

namespace ClinicAppointmentBookingSystem.Model
{
    public class SignUpRequest
    {
        public string? Name { get; set; } = string.Empty;
        public string? EmailID { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        public string? ContactNumber { get; set; } = string.Empty;
        public string? DateOfBirth { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
    }
}
