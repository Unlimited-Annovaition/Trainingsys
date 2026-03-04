using Application.DTOS.courseDTOS;
using Application.Interfaces;
using Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("getallcourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(ApiResponse<IEnumerable<CourseResponseDto>>.Ok(courses, "Courses retrieved successfully."));
        }

        [HttpGet("getcoursebyid/{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound(ApiResponse<object>.Fail("Course not found."));
            }

            return Ok(ApiResponse<CourseResponseDto>.Ok(course, "Course retrieved successfully."));
        }

        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto dto)
        {
            await _courseService.CreateCourseAsync(dto);
            return Ok(ApiResponse<object>.Ok(null, "Course created successfully."));
        }

        [HttpPut("updatecourse/{id}")] 
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto dto)
        {
            await _courseService.UpdateCourseAsync(id, dto);
            return Ok(ApiResponse<object>.Ok(null, "Course updated successfully."));
        }

        [HttpDelete("deleteCourse/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return Ok(ApiResponse<object>.Ok(null, "Course deleted successfully."));
        }

        [HttpPut("{courseId}/assign-trainer/{trainerId}")]
        public async Task<IActionResult> AssignTrainerToCourse(int courseId, int trainerId)
        {
            await _courseService.AssignTrainerToCourseAsync(courseId, trainerId);
            return Ok(ApiResponse<object>.Ok(null, "Trainer assigned to course successfully."));
        }
    }
}