using ClinicAppointmentBookingSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentBookingSystem.Service
{
    public interface IAppointmentSL
    {
        Task<AddAppointmentResponse> AddAppointment(AddAppointmentRequest request);
        Task<UpdateAppointmentResponse> UpdateAppointment(UpdateAppointmentRequest request);
        Task<GetAppointmentResponse> GetAppointment(string UserID);
        Task<DeleteAppointmentResponse> DeleteAppointment(string Id);
        Task<GetServicesListResponse> GetServicesList();
    }
}
