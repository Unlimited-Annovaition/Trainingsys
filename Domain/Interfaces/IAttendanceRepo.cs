using Domain.Entities;

namespace Domain.Interfaces;

public interface IAttendanceRepo
{
    Task<IEnumerable<Attendance>> GetAllAttendanceAsync();
    Task<Attendance?> GetAttendanceByIdAsync(int id);
    Task InsertAttendanceAsync(Attendance attendance);
    Task UpdateAttendanceAsync(Attendance attendance);
    Task DeleteAttendanceAsync(int id);
    Task<AttendancePercentageResult?> GetAttendancePercentageAsync(int enrollmentId);
}