namespace Domain.Interfaces;

using Domain.Entities;

public interface ICourseRepo
{
    Task<IEnumerable<Course>> GetAllCoursesAsync();
    Task<Course?> GetCourseByIdAsync(int id);
    Task CreateCourseAsync(Course course);
    Task UpdateCourseAsync(Course course); 
    Task DeleteCourseAsync(int id);
    Task<bool> CheckCourseTitleExistsAsync(string title);
    Task AssignTrainerToCourseAsync(int courseId, int trainerId);
    Task<bool> IsCourseFullAsync(int courseId);

}