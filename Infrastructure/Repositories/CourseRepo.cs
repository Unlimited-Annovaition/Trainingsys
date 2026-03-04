using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

using Dapper;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
class CourseSqlResult //for gets because the dapper cannot 
{
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Capacity { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; } 
    public int? TrainerId { get; set; }
    public string? TrainerName { get; set; }
}

public class CourseRepo : ICourseRepo
{
    private readonly TrainingDbContext _context;

    public CourseRepo(TrainingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        var connection = _context.Database.GetDbConnection();
        return await connection.QueryAsync<Course>(
            "sp_GetAllCourses",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        var connection = _context.Database.GetDbConnection();
        return await connection.QueryFirstOrDefaultAsync<Course>(
            "sp_GetCourseById",
            new { CourseId = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task CreateCourseAsync(Course course)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.ExecuteAsync(
            "sp_CreateCourse",
            new 
            { 
                Title = course.Title, 
                Description = course.Description, 
                Capacity = course.Capacity, 
                StartDate = course.StartDate.ToDateTime(TimeOnly.MinValue), 
                EndDate = course.EndDate.ToDateTime(TimeOnly.MinValue), 
                TrainerId = course.TrainerId 
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateCourseAsync(Course course)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.ExecuteAsync(
            "sp_UpdateCourse",
            new 
            { 
                CourseId = course.CourseId, 
                Title = course.Title, 
                Description = course.Description, 
                Capacity = course.Capacity, 
                StartDate = course.StartDate.ToDateTime(TimeOnly.MinValue), 
                EndDate = course.EndDate.ToDateTime(TimeOnly.MinValue), 
                TrainerId = course.TrainerId 
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task DeleteCourseAsync(int id)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.ExecuteAsync(
            "sp_DeleteCourse",
            new { CourseId = id },
            commandType: CommandType.StoredProcedure
        );
    }


    public async Task<bool> CheckCourseTitleExistsAsync(string title)
    {
        var connection = _context.Database.GetDbConnection();
        return await connection.ExecuteScalarAsync<bool>(
            "sp_CheckCourseTitleExists",
            new { Title = title },
            commandType: CommandType.StoredProcedure
        );
    }

    

    public async Task AssignTrainerToCourseAsync(int courseId, int trainerId)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.ExecuteAsync(
            "sp_AssignTrainerToCourse",
            new { CourseId = courseId, TrainerId = trainerId },
            commandType: CommandType.StoredProcedure
        );
    }
    public async Task<bool> IsCourseFullAsync(int courseId)
    {
        var connection = _context.Database.GetDbConnection();
        return await connection.ExecuteScalarAsync<bool>(
            "sp_IsCourseFull",
            new { CourseId = courseId },
            commandType: CommandType.StoredProcedure
        );
    }
}