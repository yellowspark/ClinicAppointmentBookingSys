namespace ClinicAppointmentBookingSystem.Model
{
    public class SignInResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public UserDetails? data { get; set; }
        public string? Token { get; set; }
        public string? User { get; set; }
    }
}
