using Application.DTOs;
using Application.DTOS;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class GradeService : IGradeService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GradeService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GradeResponseDto>> GetAllGradesAsync()
    {
        var grades = await _uow.Grades.GetAllGradesAsync();
        return _mapper.Map<IEnumerable<GradeResponseDto>>(grades);
    }

    public async Task<GradeResponseDto?> GetGradeByIdAsync(int id)
    {
        var grade = await _uow.Grades.GetGradeByIdAsync(id);
        if (grade == null) return null;
        
        return _mapper.Map<GradeResponseDto>(grade);
    }

    public async Task InsertGradeAsync(CreateGradeDto dto)
    {
        var enrollment = await _uow.Enrollments.GetEnrollmentByIdAsync(dto.EnrollmentId);
        if (enrollment == null)
            throw new Exception("Enrollment record not found. Cannot assign a grade.");

        var grade = _mapper.Map<Grade>(dto);
        await _uow.Grades.InsertGradeAsync(grade);
    }

    public async Task UpdateGradeAsync(int id, UpdateGradeDto dto)
    {
        var existing = await _uow.Grades.GetGradeByIdAsync(id);
        if (existing == null)
            throw new Exception("Grade record not found.");

        if (existing.EnrollmentId != dto.EnrollmentId)
        {
            var enrollment = await _uow.Enrollments.GetEnrollmentByIdAsync(dto.EnrollmentId);
            if (enrollment == null)
                throw new Exception("The specified Enrollment record was not found.");
        }

        existing.EnrollmentId = dto.EnrollmentId;
        existing.Score = dto.Score;
        existing.Evaluation = dto.Evaluation;

        await _uow.Grades.UpdateGradeAsync(existing);
    }

    public async Task DeleteGradeAsync(int id)
    {
        var existing = await _uow.Grades.GetGradeByIdAsync(id);
        if (existing == null)
            throw new Exception("Grade record not found.");

        await _uow.Grades.DeleteGradeAsync(id);
    }

    public async Task<TraineeGradeReportDto?> GetTraineeGradeReportAsync(int traineeId, int courseId)
    {
        var report = await _uow.Grades.GetTraineeGradeReportAsync(traineeId, courseId);
        if (report == null) return null;

        return _mapper.Map<TraineeGradeReportDto>(report);
    }
}