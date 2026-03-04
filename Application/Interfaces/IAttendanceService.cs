using Application.DTOs;
using Application.DTOS;

namespace Application.Interfaces;

public interface IAttendanceService
{
    Task<IEnumerable<AttendanceResponseDto>> GetAllAttendanceAsync();
    Task<AttendanceResponseDto?> GetAttendanceByIdAsync(int id);
    Task InsertAttendanceAsync(CreateAttendanceDto dto);
    Task UpdateAttendanceAsync(int id, UpdateAttendanceDto dto);
    Task DeleteAttendanceAsync(int id);
    Task<AttendancePercentageDto?> GetAttendancePercentageAsync(int enrollmentId);
}