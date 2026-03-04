using Domain.Entities;

namespace Domain.Interfaces;

public interface IGradeRepo
{
    Task<IEnumerable<Grade>> GetAllGradesAsync();
    Task<Grade?> GetGradeByIdAsync(int id);
    Task InsertGradeAsync(Grade grade);
    Task UpdateGradeAsync(Grade grade);
    Task DeleteGradeAsync(int id);
    Task<TraineeGradeReportResult?> GetTraineeGradeReportAsync(int traineeId, int courseId);
}