using ClinicAppointmentBookingSystem.Model;
using ClinicAppointmentBookingSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentBookingSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentSL _appointmentSL;
        public AppointmentController(IAppointmentSL appointmentSL) 
        {
            _appointmentSL = appointmentSL;
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment(AddAppointmentRequest request)
        {
            AddAppointmentResponse response = new AddAppointmentResponse();
            try
            {
                response = await _appointmentSL.AddAppointment(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAppointment(UpdateAppointmentRequest request)
        {
            UpdateAppointmentResponse response = new UpdateAppointmentResponse();
            try
            {
                response = await _appointmentSL.UpdateAppointment(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointment([FromQuery]string UserID)
        {
            GetAppointmentResponse response = new GetAppointmentResponse();
            try
            {
                response = await _appointmentSL.GetAppointment(UserID);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAppointment([FromQuery]string Id)
        {
            DeleteAppointmentResponse response = new DeleteAppointmentResponse();
            try
            {
                response = await _appointmentSL.DeleteAppointment(Id);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetServicesList()
        {
            GetServicesListResponse response = new GetServicesListResponse();
            try
            {
                response = await _appointmentSL.GetServicesList();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }


    }
}
