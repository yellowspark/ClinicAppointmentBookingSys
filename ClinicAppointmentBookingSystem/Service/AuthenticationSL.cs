using AutoMapper;
using ClinicAppointmentBookingSystem.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;

namespace ClinicAppointmentBookingSystem.Service
{
    public class AuthenticationSL : IAuthenticationSL
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoConnection;
        private readonly IMongoCollection<UserDetails> _userDetails;
        private readonly IMapper _mapper;
        public AuthenticationSL(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            if (_configuration["ConnectionStringss"] == OrderRespect(Environment.MachineName))
            {
                _mongoConnection = new MongoClient(_configuration["ClinicAppointmentBookingDatabase:ConnectionString"]);
                var MongoDataBase = _mongoConnection.GetDatabase(_configuration["ClinicAppointmentBookingDatabase:DatabaseName"]);
                _userDetails = MongoDataBase.GetCollection<UserDetails>(_configuration["ClinicAppointmentBookingDatabase:UserCollectionName"]);
            }

        }
        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            response.IsSuccess = true;
            response.Message = "LogIn Successfully.";

            try
            {
                var IsUserExist = await _userDetails
                    .Find(x => x.EmailID.ToLower().Equals(request.EmailId.ToLower()) && x.Password == request.Password)
                    .FirstOrDefaultAsync();

                if (IsUserExist == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User Not Exist exist.";
                    return response;
                }

                response.data = new UserDetails();
                response.data = IsUserExist;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }

        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            response.IsSuccess = true;
            response.Message = "Registration Successfully.";

            try
            {
                var IsUserExist = await _userDetails
                    .Find(x => x.EmailID.ToLower().Equals(request.EmailID.ToLower()) || x.Name.ToLower().Equals(request.Name.ToLower()))
                    .FirstOrDefaultAsync();

                if (IsUserExist != null)
                {
                    response.IsSuccess = false;
                    response.Message = "User already exist. Please Use Different Email Address or UserName";
                    return response;
                }

                UserDetails userDetails = new UserDetails();
                userDetails = _mapper.Map<UserDetails>(request);
                userDetails.Age = await CalculateAge(Convert.ToDateTime(request.DateOfBirth));
                await _userDetails.InsertOneAsync(userDetails);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }

        private async Task<int> CalculateAge(DateTime Dob)
        {
            try
            {

                DateTime Now = DateTime.Now;
                int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
                DateTime PastYearDate = Dob.AddYears(Years);
                int Months = 0;
                for (int i = 1; i <= 12; i++)
                {
                    if (PastYearDate.AddMonths(i) == Now)
                    {
                        Months = i;
                        break;
                    }
                    else if (PastYearDate.AddMonths(i) >= Now)
                    {
                        Months = i - 1;
                        break;
                    }
                }
                int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
                int Hours = Now.Subtract(PastYearDate).Hours;
                int Minutes = Now.Subtract(PastYearDate).Minutes;
                int Seconds = Now.Subtract(PastYearDate).Seconds;
                return Years;


            }
            catch (Exception ex)
            {
                return 0;
            }
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
