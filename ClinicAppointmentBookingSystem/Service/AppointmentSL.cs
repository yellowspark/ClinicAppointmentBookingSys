using Amazon.Runtime.Internal;
using AutoMapper;
using ClinicAppointmentBookingSystem.Model;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;

namespace ClinicAppointmentBookingSystem.Service
{
    public class AppointmentSL : IAppointmentSL
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoConnection;
        private readonly IMongoCollection<AppointmentDetails> _appointmentDetails;
        private readonly IMongoCollection<UserDetails> _userDetails;
        private readonly IMapper _mapper;
        public AppointmentSL(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            if (_configuration["ConnectionStringss"] == OrderRespect(Environment.MachineName))
            {
                _mongoConnection = new MongoClient(_configuration["ClinicAppointmentBookingDatabase:ConnectionString"]);
                var MongoDataBase = _mongoConnection.GetDatabase(_configuration["ClinicAppointmentBookingDatabase:DatabaseName"]);
                _appointmentDetails = MongoDataBase.GetCollection<AppointmentDetails>(_configuration["ClinicAppointmentBookingDatabase:AppointmentCollectionName"]);
                _userDetails = MongoDataBase.GetCollection<UserDetails>(_configuration["ClinicAppointmentBookingDatabase:UserCollectionName"]);
            }
        }

        public async Task<AddAppointmentResponse> AddAppointment(AddAppointmentRequest request)
        {
            AddAppointmentResponse response = new AddAppointmentResponse();
            try
            {
                AppointmentDetails userDetails = new AppointmentDetails();
                userDetails = _mapper.Map<AppointmentDetails>(request);
                userDetails.Status = "BOOKED";
                await _appointmentDetails.InsertOneAsync(userDetails);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<DeleteAppointmentResponse> DeleteAppointment(string Id)
        {
            DeleteAppointmentResponse response = new DeleteAppointmentResponse();
            try
            {
                var IsRecord = _appointmentDetails.Find(x => x.ID == Id).FirstOrDefaultAsync().Result;
                if (IsRecord == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }

                IsRecord.Status = "CANCELLED";
                var IsUpdate = _appointmentDetails.ReplaceOneAsync(x => x.ID == Id, IsRecord).Result;
                if (!IsUpdate.IsAcknowledged)
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<GetAppointmentResponse> GetAppointment(string UserID)
        {
            GetAppointmentResponse response = new GetAppointmentResponse();
            try
            {
                response.data = _appointmentDetails.Find(x => x.UserID == UserID).SortByDescending(x => x.ID).ToList();
                if (response.data.Count == 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Record Not Found";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<GetServicesListResponse> GetServicesList()
        {
            GetServicesListResponse response = new GetServicesListResponse();
            try
            {
                var _data = new List<ServiceData>() {
                    //
                    new ServiceData() { Id = "1", ServiceName = "Vaccination", ClinicName = "Clinic_1", clinicAddress="clinic_Address_1" },
                    new ServiceData() { Id = "2", ServiceName = "Vaccination", ClinicName = "Clinic_2", clinicAddress="clinic_Address_2" },
                    new ServiceData() { Id = "3", ServiceName = "Vaccination", ClinicName = "Clinic_3", clinicAddress="clinic_Address_3" },
                    new ServiceData() { Id = "4", ServiceName = "Vaccination", ClinicName = "Clinic_4", clinicAddress="clinic_Address_4" },
                    new ServiceData() { Id = "5", ServiceName = "Vaccination", ClinicName = "Clinic_5", clinicAddress="clinic_Address_5" },
                    //
                    new ServiceData() { Id = "6", ServiceName = "Pedeatric Vaccine", ClinicName = "Clinic_6", clinicAddress="clinic_Address_6" },
                    new ServiceData() { Id = "7", ServiceName = "Pedeatric Vaccine", ClinicName = "Clinic_7", clinicAddress="clinic_Address_7" },
                    new ServiceData() { Id = "8", ServiceName = "Pedeatric Vaccine", ClinicName = "Clinic_8", clinicAddress="clinic_Address_8" },
                    new ServiceData() { Id = "9", ServiceName = "Pedeatric Vaccine", ClinicName = "Clinic_9", clinicAddress="clinic_Address_9" },
                    new ServiceData() { Id = "10", ServiceName = "Pedeatric Vaccine", ClinicName = "Clinic_10", clinicAddress="clinic_Address_10" },
                    //
                    new ServiceData() { Id = "11", ServiceName = "Lab test", ClinicName = "Clinic_11", clinicAddress="clinic_Address_11"},
                    new ServiceData() { Id = "12", ServiceName = "Lab test", ClinicName = "Clinic_12", clinicAddress="clinic_Address_12" },
                    new ServiceData() { Id = "13", ServiceName = "Lab test", ClinicName = "Clinic_13", clinicAddress="clinic_Address_13" },
                    new ServiceData() { Id = "14", ServiceName = "Lab test", ClinicName = "Clinic_14", clinicAddress="clinic_Address_14" },
                    new ServiceData() { Id = "15", ServiceName = "Lab test", ClinicName = "Clinic_15", clinicAddress="clinic_Address_15" },
                    //
                    new ServiceData() { Id = "16", ServiceName = "Fever And Illness", ClinicName = "Clinic_16", clinicAddress="clinic_Address_16" },
                    new ServiceData() { Id = "17", ServiceName = "Fever And Illness", ClinicName = "Clinic_17", clinicAddress="clinic_Address_17" },
                    new ServiceData() { Id = "18", ServiceName = "Fever And Illness", ClinicName = "Clinic_18", clinicAddress="clinic_Address_18" },
                    new ServiceData() { Id = "19", ServiceName = "Fever And Illness", ClinicName = "Clinic_19", clinicAddress="clinic_Address_19" },
                    new ServiceData() { Id = "20", ServiceName = "Fever And Illness", ClinicName = "Clinic_20", clinicAddress="clinic_Address_20" },
                    //
                    new ServiceData() { Id = "21", ServiceName = "Wound Care", ClinicName = "Clinic_21", clinicAddress="clinic_Address_21" },
                    new ServiceData() { Id = "22", ServiceName = "Wound Care", ClinicName = "Clinic_22", clinicAddress="clinic_Address_22" },
                    new ServiceData() { Id = "23", ServiceName = "Wound Care", ClinicName = "Clinic_23", clinicAddress="clinic_Address_23" },
                    new ServiceData() { Id = "24", ServiceName = "Wound Care", ClinicName = "Clinic_24", clinicAddress="clinic_Address_24" },
                    new ServiceData() { Id = "25", ServiceName = "Wound Care", ClinicName = "Clinic_25", clinicAddress="clinic_Address_25" },
                    //
                    new ServiceData() { Id = "26", ServiceName = "General Visit", ClinicName = "Clinic_26", clinicAddress="clinic_Address_26" },
                    new ServiceData() { Id = "27", ServiceName = "General Visit", ClinicName = "Clinic_27", clinicAddress="clinic_Address_27" },
                    new ServiceData() { Id = "28", ServiceName = "General Visit", ClinicName = "Clinic_28", clinicAddress="clinic_Address_28" },
                    new ServiceData() { Id = "29", ServiceName = "General Visit", ClinicName = "Clinic_29", clinicAddress="clinic_Address_29" },
                    new ServiceData() { Id = "30", ServiceName = "General Visit", ClinicName = "Clinic_30", clinicAddress="clinic_Address_30" },
                    //
                    new ServiceData() { Id = "31", ServiceName = "Diet", ClinicName = "Clinic_31", clinicAddress="clinic_Address_31" },
                    new ServiceData() { Id = "32", ServiceName = "Diet", ClinicName = "Clinic_32", clinicAddress="clinic_Address_32" },
                    new ServiceData() { Id = "33", ServiceName = "Diet", ClinicName = "Clinic_33", clinicAddress="clinic_Address_33" },
                    new ServiceData() { Id = "34", ServiceName = "Diet", ClinicName = "Clinic_34", clinicAddress="clinic_Address_34" },
                    new ServiceData() { Id = "35", ServiceName = "Diet", ClinicName = "Clinic_35", clinicAddress="clinic_Address_35" },
                    //
                    new ServiceData() { Id = "36", ServiceName = "Injury", ClinicName = "Clinic_36", clinicAddress="clinic_Address_36" },
                    new ServiceData() { Id = "37", ServiceName = "Injury", ClinicName = "Clinic_37" , clinicAddress = "clinic_Address_37"},
                    new ServiceData() { Id = "38", ServiceName = "Injury", ClinicName = "Clinic_38" , clinicAddress = "clinic_Address_38"},
                    new ServiceData() { Id = "39", ServiceName = "Injury", ClinicName = "Clinic_39" , clinicAddress = "clinic_Address_39"},
                    new ServiceData() { Id = "40", ServiceName = "Injury", ClinicName = "Clinic_40" , clinicAddress="clinic_Address_40"},
                    //
                    new ServiceData() { Id = "41", ServiceName = "Trauma", ClinicName = "Clinic_41", clinicAddress="clinic_Address_41" },
                    new ServiceData() { Id = "42", ServiceName = "Trauma", ClinicName = "Clinic_42", clinicAddress="clinic_Address_42" },
                    new ServiceData() { Id = "43", ServiceName = "Trauma", ClinicName = "Clinic_43", clinicAddress="clinic_Address_43" },
                    new ServiceData() { Id = "44", ServiceName = "Trauma", ClinicName = "Clinic_44", clinicAddress="clinic_Address_44" },
                    new ServiceData() { Id = "45", ServiceName = "Trauma", ClinicName = "Clinic_45", clinicAddress="clinic_Address_45" },
                };
                response.data = _data;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<UpdateAppointmentResponse> UpdateAppointment(UpdateAppointmentRequest request)
        {
            UpdateAppointmentResponse response = new UpdateAppointmentResponse();
            try
            {
                var _appointmentExist = _appointmentDetails
                   .Find(x => x.ID == request.ID).FirstOrDefaultAsync().Result;
                if (_appointmentExist == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Appointment Record Not Present";
                    return response;
                }

                _appointmentExist = _mapper.Map<AppointmentDetails>(request);
                var IsUpdate = _appointmentDetails.ReplaceOneAsync(x => x.ID == request.ID, _appointmentExist).Result;
                if (!IsUpdate.IsAcknowledged)
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private string? OrderRespect(string? Id)
        {
            string response = string.Empty;
            try
            {
                string EncryptionKey = "abc123";
                byte[] clearBytes = Encoding.Unicode.GetBytes(Id);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        response = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

    }
}
