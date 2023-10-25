namespace ClinicAppointmentBookingSystem.Model
{
    public class GetServicesListResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; } = string.Empty;
        public List<ServiceData> data { get; set; } = new List<ServiceData>();
    }

    public class ServiceData
    {
        public string Id { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string ClinicName { get; set; } = string.Empty;
        public string clinicAddress { get; set; } = string.Empty;
    }
}
