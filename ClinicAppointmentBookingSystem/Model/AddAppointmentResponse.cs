namespace ClinicAppointmentBookingSystem.Model
{
    public class AddAppointmentResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; } = "Add Appointment Successfully";
    }
}
