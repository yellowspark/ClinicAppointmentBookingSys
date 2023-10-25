namespace ClinicAppointmentBookingSystem.Model
{
    public class GetAppointmentResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; } = "Sucessfully";
        public List<AppointmentDetails> data { get; set; } = new List<AppointmentDetails>();
    }
}
