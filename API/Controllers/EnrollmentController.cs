using Application.Interfaces;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet("getEnrollments")]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
            return Ok(ApiResponse<IEnumerable<EnrollmentResponseDto>>.Ok(enrollments, "Enrollments retrieved successfully."));
        }

        [HttpGet("getenrollementbyid/{id}")]
        public async Task<IActionResult> GetEnrollmentById(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound(ApiResponse<object>.Fail("Enrollment not found."));
            }
            return Ok(ApiResponse<EnrollmentResponseDto>.Ok(enrollment, "Enrollment retrieved successfully."));
        }

        [HttpPost("InsertEnrollment")]
        public async Task<IActionResult> CreateEnrollment([FromBody] CreateEnrollmentDto dto)
        {
            await _enrollmentService.InsertEnrollmentAsync(dto);
            return Ok(ApiResponse<object>.Ok(null, "Trainee enrolled successfully."));
        }

        [HttpPut("UpdateEnrollment/{id}")]
        public async Task<IActionResult> UpdateEnrollment(int id, [FromBody] UpdateEnrollmentDto dto)
        {
            await _enrollmentService.UpdateEnrollmentAsync(id, dto);
            return Ok(ApiResponse<object>.Ok(null, "Enrollment updated successfully."));
        }

        [HttpDelete("DeleteEnrollment/{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            await _enrollmentService.DeleteEnrollmentAsync(id);
            return Ok(ApiResponse<object>.Ok(null, "Enrollment deleted successfully."));
        }
    }
}