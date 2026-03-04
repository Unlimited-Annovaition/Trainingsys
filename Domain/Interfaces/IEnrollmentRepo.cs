using Domain.Entities;

namespace Domain.Interfaces;

public interface IEnrollmentRepo
{
    Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync();
    Task<Enrollment?> GetEnrollmentByIdAsync(int id);
    Task InsertEnrollmentAsync(Enrollment enrollment);
    Task UpdateEnrollmentAsync(Enrollment enrollment);
    Task DeleteEnrollmentAsync(int id);
    Task<bool> CheckDuplicateEnrollmentAsync(int courseId, int traineeId);
}