using AutoMapper;
using ClinicAppointmentBookingSystem.Model;

namespace Member_Registration_Portal_.Net.Mapper
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            
            CreateMap<SignUpRequest, UserDetails>();
        }
    }
}
