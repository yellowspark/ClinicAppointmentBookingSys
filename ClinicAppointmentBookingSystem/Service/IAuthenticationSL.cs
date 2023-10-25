using ClinicAppointmentBookingSystem.Model;

namespace ClinicAppointmentBookingSystem.Service
{
    public interface IAuthenticationSL
    {
        Task<SignUpResponse> SignUp(SignUpRequest request);
        Task<SignInResponse> SignIn(SignInRequest request);
    }
}
