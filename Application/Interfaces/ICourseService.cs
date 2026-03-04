using Application.DTOS.courseDTOS;

namespace Application.Interfaces;
public interface ICourseService
{
    Task<IEnumerable<CourseResponseDto>> GetAllCoursesAsync();
    Task<CourseResponseDto?> GetCourseByIdAsync(int id);
    Task CreateCourseAsync(CreateCourseDto dto);
    Task UpdateCourseAsync(int id, UpdateCourseDto dto);
    Task DeleteCourseAsync(int id);
    Task AssignTrainerToCourseAsync(int courseId, int trainerId);
}