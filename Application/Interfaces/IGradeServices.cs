using Application.DTOs;
using Application.DTOS;

namespace Application.Interfaces;

public interface IGradeService
{
    Task<IEnumerable<GradeResponseDto>> GetAllGradesAsync();
    Task<GradeResponseDto?> GetGradeByIdAsync(int id);
    Task InsertGradeAsync(CreateGradeDto dto);
    Task UpdateGradeAsync(int id, UpdateGradeDto dto);
    Task DeleteGradeAsync(int id);
    Task<TraineeGradeReportDto?> GetTraineeGradeReportAsync(int traineeId, int courseId);
}