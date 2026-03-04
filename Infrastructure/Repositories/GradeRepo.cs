using Dapper;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class GradeRepository : IGradeRepo
{
    private readonly TrainingDbContext _context;

    public GradeRepository(TrainingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Grade>> GetAllGradesAsync()
    {
        var connection = _context.Database.GetDbConnection();
        return await connection.QueryAsync<Grade>(
            "sp_GetAllGrades",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Grade?> GetGradeByIdAsync(int id)
    {
        var connection = _context.Database.GetDbConnection();
        return await connection.QueryFirstOrDefaultAsync<Grade>(
            "sp_GetGradeById",
            new { GradeId = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task InsertGradeAsync(Grade grade)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.ExecuteAsync(
            "sp_InsertGrade",
            new 
            { 
                EnrollmentId = grade.EnrollmentId, 
                Score = grade.Score, 
                Evaluation = grade.Evaluation 
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateGradeAsync(Grade grade)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.ExecuteAsync(
            "sp_UpdateGrade",
            new 
            { 
                GradeId = grade.GradeId,
                EnrollmentId = grade.EnrollmentId, 
                Score = grade.Score, 
                Evaluation = grade.Evaluation 
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task DeleteGradeAsync(int id)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.ExecuteAsync(
            "sp_DeleteGrade",
            new { GradeId = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<TraineeGradeReportResult?> GetTraineeGradeReportAsync(int traineeId, int courseId)
    {
        var connection = _context.Database.GetDbConnection();
        return await connection.QueryFirstOrDefaultAsync<TraineeGradeReportResult>(
            "sp_GetTraineeGradeReport",
            new { TraineeId = traineeId, CourseId = courseId },
            commandType: CommandType.StoredProcedure
        );
    }
}