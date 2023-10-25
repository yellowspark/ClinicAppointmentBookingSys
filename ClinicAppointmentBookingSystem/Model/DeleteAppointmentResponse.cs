namespace ClinicAppointmentBookingSystem.Model
{
    public class DeleteAppointmentResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; } = "Appointment Cancel Successfully";
    }
}
