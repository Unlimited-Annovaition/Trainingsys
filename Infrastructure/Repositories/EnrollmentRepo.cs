using System.Data;
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

    public class EnrollmentRepository : IEnrollmentRepo
{
    private readonly TrainingDbContext _context;

    public EnrollmentRepository(TrainingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
    {
        var connection = _context.Database.GetDbConnection();
        return await connection.QueryAsync<Enrollment>(
            "sp_GetAllEnrollments",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Enrollment?> GetEnrollmentByIdAsync(int id)
    {
        var connection = _context.Database.GetDbConnection();
        return await connection.QueryFirstOrDefaultAsync<Enrollment>(
            "sp_GetEnrollmentById",
            new { EnrollmentId = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task InsertEnrollmentAsync(Enrollment enrollment)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.ExecuteAsync(
            "sp_InsertEnrollment",
            new 
            { 
                CourseId = enrollment.CourseId, 
                TraineeId = enrollment.TraineeId, 
                EnrollmentDate = enrollment.EnrollmentDate 
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateEnrollmentAsync(Enrollment enrollment)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.ExecuteAsync(
            "sp_UpdateEnrollment",
            new 
            { 
                EnrollmentId = enrollment.EnrollmentId,
                CourseId = enrollment.CourseId, 
                TraineeId = enrollment.TraineeId, 
                EnrollmentDate = enrollment.EnrollmentDate 
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task DeleteEnrollmentAsync(int id)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.ExecuteAsync(
            "sp_DeleteEnrollment",
            new { EnrollmentId = id },
            commandType: CommandType.StoredProcedure
        );
    }
    public async Task<bool> CheckDuplicateEnrollmentAsync(int courseId, int traineeId)
    {
        var connection = _context.Database.GetDbConnection();
        return await connection.ExecuteScalarAsync<bool>(
            "sp_CheckDuplicateEnrollment",
            new { CourseId = courseId, TraineeId = traineeId },
            commandType: CommandType.StoredProcedure
        );
    }

    
}