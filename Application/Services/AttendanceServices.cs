using Application.DTOs;
using Application.DTOS;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public AttendanceService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AttendanceResponseDto>> GetAllAttendanceAsync()
    {
        var attendanceList = await _uow.Attendances.GetAllAttendanceAsync();
        return _mapper.Map<IEnumerable<AttendanceResponseDto>>(attendanceList);
    }

    public async Task<AttendanceResponseDto?> GetAttendanceByIdAsync(int id)
    {
        var attendance = await _uow.Attendances.GetAttendanceByIdAsync(id);
        if (attendance == null) return null;
        
        return _mapper.Map<AttendanceResponseDto>(attendance);
    }

    public async Task InsertAttendanceAsync(CreateAttendanceDto dto)
    {
        var enrollment = await _uow.Enrollments.GetEnrollmentByIdAsync(dto.EnrollmentId);
        if (enrollment == null)
            throw new Exception("Enrollment record not found. Cannot add attendance.");

        var attendance = _mapper.Map<Attendance>(dto);
        await _uow.Attendances.InsertAttendanceAsync(attendance);
    }

    public async Task UpdateAttendanceAsync(int id, UpdateAttendanceDto dto)
    {
        var existing = await _uow.Attendances.GetAttendanceByIdAsync(id);
        if (existing == null)
            throw new Exception("Attendance record not found.");

        if (existing.EnrollmentId != dto.EnrollmentId)
        {
            var enrollment = await _uow.Enrollments.GetEnrollmentByIdAsync(dto.EnrollmentId);
            if (enrollment == null)
                throw new Exception("The specified Enrollment record was not found.");
        }

        existing.EnrollmentId = dto.EnrollmentId;
        existing.Date = DateOnly.FromDateTime(dto.Date);
        existing.Status = dto.Status;

        await _uow.Attendances.UpdateAttendanceAsync(existing);
    }

    public async Task DeleteAttendanceAsync(int id)
    {
        var existing = await _uow.Attendances.GetAttendanceByIdAsync(id);
        if (existing == null)
            throw new Exception("Attendance record not found.");

        await _uow.Attendances.DeleteAttendanceAsync(id);
    }

    public async Task<AttendancePercentageDto?> GetAttendancePercentageAsync(int enrollmentId)
    {
        var result = await _uow.Attendances.GetAttendancePercentageAsync(enrollmentId);
        if (result == null) return null;

        return _mapper.Map<AttendancePercentageDto>(result);
    }
}