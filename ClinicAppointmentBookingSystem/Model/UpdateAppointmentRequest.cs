namespace ClinicAppointmentBookingSystem.Model
{
    public class UpdateAppointmentRequest
    {
        public string? ID { get; set; }
        public string? UserID { get; set; }
        public string? ClientName { get; set; }
        public string? AppointmentDate { get; set; }
        public string? AppointmentTime { get; set; }
        public string? ClinicName { get; set; }
        public string? ClinicAddress { get; set; }
        public string? Service { get; set; }
    }
}
