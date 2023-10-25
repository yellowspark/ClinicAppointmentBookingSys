using MongoDB.Bson.Serialization.Attributes;

namespace ClinicAppointmentBookingSystem.Model
{
    public class AppointmentDetails
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? ID { get; set; }
        public string? UserID { get; set; }
        public string? CreatedDate { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");
        public string? ClientName {  get; set; }
        public string? AppointmentDate { get; set; }
        public string? AppointmentTime { get; set; }
        public string? ClinicName { get; set; }
        public string? ClinicAddress { get; set; }
        public string? Service {  get; set; }
        public string? Status { get; set; } = "BOOKED";
        public bool IsActive { get; set; } = true;
    }
}
