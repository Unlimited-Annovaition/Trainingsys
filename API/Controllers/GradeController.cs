using Application.DTOs;
using Application.DTOS;
using Application.Interfaces;
using Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet("GetAllGrades")]
        public async Task<IActionResult> GetAllGrades()
        {
            var grades = await _gradeService.GetAllGradesAsync();
            return Ok(ApiResponse<IEnumerable<GradeResponseDto>>.Ok(grades, "Grades retrieved successfully."));
        }

        [HttpGet("GetGradeById/{id}")]
        public async Task<IActionResult> GetGradeById(int id)
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
                return NotFound(ApiResponse<object>.Fail("Grade record not found."));

            return Ok(ApiResponse<GradeResponseDto>.Ok(grade, "Grade retrieved successfully."));
        }

        [HttpGet("GetTraineeGradeReport/{traineeId}/{courseId}")]
        public async Task<IActionResult> GetTraineeGradeReport(int traineeId, int courseId)
        {
            var report = await _gradeService.GetTraineeGradeReportAsync(traineeId, courseId);
            if (report == null)
                return NotFound(ApiResponse<object>.Fail("No grade report found for the specified trainee and course."));

            return Ok(ApiResponse<TraineeGradeReportDto>.Ok(report, "Trainee grade report generated successfully."));
        }

        [HttpPost("CreateGrade")]
        public async Task<IActionResult> CreateGrade([FromBody] CreateGradeDto dto)
        {
            await _gradeService.InsertGradeAsync(dto);
            return Ok(ApiResponse<object>.Ok(null, "Grade recorded successfully."));
        }

        [HttpPut("UpdateGrade/{id}")]
        public async Task<IActionResult> UpdateGrade(int id, [FromBody] UpdateGradeDto dto)
        {
            await _gradeService.UpdateGradeAsync(id, dto);
            return Ok(ApiResponse<object>.Ok(null, "Grade updated successfully."));
        }

        [HttpDelete("DeleteGrade/{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            await _gradeService.DeleteGradeAsync(id);
            return Ok(ApiResponse<object>.Ok(null, "Grade deleted successfully."));
        }
    }
}