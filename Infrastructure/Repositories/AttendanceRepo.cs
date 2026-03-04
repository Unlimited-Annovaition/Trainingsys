using Dapper;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class AttendanceRepository : IAttendanceRepo
{
    private readonly TrainingDbContext _context;

    public AttendanceRepository(TrainingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Attendance>> GetAllAttendanceAsync()
    {
        var connection = _context.Database.GetDbConnection();
        return await connection.QueryAsync<Attendance>(
            "sp_GetAllAttendance",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Attendance?> GetAttendanceByIdAsync(int id)
    {
        var connection = _context.Database.GetDbConnection();
        return await connection.QueryFirstOrDefaultAsync<Attendance>(
            "sp_GetAttendanceById",
            new { AttendanceId = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task InsertAttendanceAsync(Attendance attendance)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.ExecuteAsync(
            "sp_InsertAttendance",
            new 
            { 
                EnrollmentId = attendance.EnrollmentId, 
                Date = attendance.Date, 
                Status = attendance.Status 
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateAttendanceAsync(Attendance attendance)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.ExecuteAsync(
            "sp_UpdateAttendance",
            new 
            { 
                AttendanceId = attendance.AttendanceId,
                EnrollmentId = attendance.EnrollmentId, 
                Date = attendance.Date, 
                Status = attendance.Status 
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task DeleteAttendanceAsync(int id)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.ExecuteAsync(
            "sp_DeleteAttendance",
            new { AttendanceId = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<AttendancePercentageResult?> GetAttendancePercentageAsync(int enrollmentId)
    {
        var connection = _context.Database.GetDbConnection();
        return await connection.QueryFirstOrDefaultAsync<AttendancePercentageResult>(
            "sp_GetAttendancePercentage",
            new { EnrollmentId = enrollmentId },
            commandType: CommandType.StoredProcedure
        );
    }
}