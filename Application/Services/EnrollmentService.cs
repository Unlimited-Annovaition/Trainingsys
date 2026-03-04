using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public EnrollmentService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EnrollmentResponseDto>> GetAllEnrollmentsAsync()
    {
        var enrollments = await _uow.Enrollments.GetAllEnrollmentsAsync();
        return _mapper.Map<IEnumerable<EnrollmentResponseDto>>(enrollments);
    }

    public async Task<EnrollmentResponseDto?> GetEnrollmentByIdAsync(int id)
    {
        var enrollment = await _uow.Enrollments.GetEnrollmentByIdAsync(id);
        if (enrollment == null) return null;

        return _mapper.Map<EnrollmentResponseDto>(enrollment);
    }

    public async Task InsertEnrollmentAsync(CreateEnrollmentDto dto)
    {
        bool isDuplicate = await _uow.Enrollments.CheckDuplicateEnrollmentAsync(dto.CourseId, dto.TraineeId);
        if (isDuplicate)
            throw new Exception("Trainee is already enrolled in this course.");

        bool isFull = await _uow.Courses.IsCourseFullAsync(dto.CourseId);
        if (isFull)
            throw new Exception("Course capacity is full. Cannot enroll more trainees.");

        var enrollment = _mapper.Map<Enrollment>(dto);
        await _uow.Enrollments.InsertEnrollmentAsync(enrollment);
        await _uow.CompleteAsync();

    }

    public async Task UpdateEnrollmentAsync(int id, UpdateEnrollmentDto dto)
    {
        var existingEnrollment = await _uow.Enrollments.GetEnrollmentByIdAsync(id);
        if (existingEnrollment == null)
            throw new Exception("Enrollment not found.");

        if (existingEnrollment.CourseId != dto.CourseId || existingEnrollment.TraineeId != dto.TraineeId)
        {
            bool isDuplicate = await _uow.Enrollments.CheckDuplicateEnrollmentAsync(dto.CourseId, dto.TraineeId);
            if (isDuplicate)
                throw new Exception("Trainee is already enrolled in this course.");

            if (existingEnrollment.CourseId != dto.CourseId)
            {
                bool isFull = await _uow.Courses.IsCourseFullAsync(dto.CourseId);
                if (isFull)
                    throw new Exception("New course capacity is full.");
            }
        }

        existingEnrollment.CourseId = dto.CourseId;
        existingEnrollment.TraineeId = dto.TraineeId;
        existingEnrollment.EnrollmentDate = dto.EnrollmentDate;

        await _uow.Enrollments.UpdateEnrollmentAsync(existingEnrollment);
        await _uow.CompleteAsync();

    }

    public async Task DeleteEnrollmentAsync(int id)
    {
        var enrollment = await _uow.Enrollments.GetEnrollmentByIdAsync(id);
        if (enrollment == null)
            throw new Exception("Enrollment not found.");

        await _uow.Enrollments.DeleteEnrollmentAsync(id);
        await _uow.CompleteAsync();
    }
}