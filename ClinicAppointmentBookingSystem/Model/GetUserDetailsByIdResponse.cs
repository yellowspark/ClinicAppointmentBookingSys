namespace ClinicAppointmentBookingSystem.Model
{
    public class GetUserDetailsByIdResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; } = "Sucessfully";
        public UserDetails? data { get; set; } = new UserDetails();
    }
}
