using Application.DTOS;
using Application.Interfaces;
using Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet("getAllAttendances")]
        public async Task<IActionResult> GetAllAttendance()
        {
            var attendanceList = await _attendanceService.GetAllAttendanceAsync();
            return Ok(ApiResponse<IEnumerable<AttendanceResponseDto>>.Ok(attendanceList, "Attendance records retrieved successfully."));
        }

        [HttpGet("getAttendanceById/{id}")]
        public async Task<IActionResult> GetAttendanceById(int id)
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
            if (attendance == null)
                return NotFound(ApiResponse<object>.Fail("Attendance record not found."));

            return Ok(ApiResponse<AttendanceResponseDto>.Ok(attendance, "Attendance record retrieved successfully."));
        }

        [HttpGet("percentage/{enrollmentId}")]
        public async Task<IActionResult> GetAttendancePercentage(int enrollmentId)
        {
            var percentageResult = await _attendanceService.GetAttendancePercentageAsync(enrollmentId);
            if (percentageResult == null)
                return NotFound(ApiResponse<object>.Fail("No attendance records found for this enrollment."));

            return Ok(ApiResponse<AttendancePercentageDto>.Ok(percentageResult, "Attendance percentage calculated successfully."));
        }

        [HttpPost("InsertAttendance")]
        public async Task<IActionResult> CreateAttendance([FromBody] CreateAttendanceDto dto)
        {
            await _attendanceService.InsertAttendanceAsync(dto);
            return Ok(ApiResponse<object>.Ok(null, "Attendance recorded successfully."));
        }

        [HttpPut("UpdateAttendance/{id}")]
        public async Task<IActionResult> UpdateAttendance(int id, [FromBody] UpdateAttendanceDto dto)
        {
            await _attendanceService.UpdateAttendanceAsync(id, dto);
            return Ok(ApiResponse<object>.Ok(null, "Attendance updated successfully."));
        }

        [HttpDelete("DeleteAttendance/{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            await _attendanceService.DeleteAttendanceAsync(id);
            return Ok(ApiResponse<object>.Ok(null, "Attendance deleted successfully."));
        }
    }
}