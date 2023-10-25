using AutoMapper;
using ClinicAppointmentBookingSystem.Model;

namespace ClinicAppointmentBookingSystem.Mapper
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<AddAppointmentRequest, AppointmentDetails>();
            CreateMap<UpdateAppointmentRequest, AppointmentDetails>();

        }
    }
}
