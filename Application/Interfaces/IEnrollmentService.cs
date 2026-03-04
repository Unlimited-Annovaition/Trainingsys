namespace Application.Interfaces;

public interface IEnrollmentService
{
    Task<IEnumerable<EnrollmentResponseDto>> GetAllEnrollmentsAsync();
    Task<EnrollmentResponseDto?> GetEnrollmentByIdAsync(int id);
    Task InsertEnrollmentAsync(CreateEnrollmentDto dto);
    Task UpdateEnrollmentAsync(int id, UpdateEnrollmentDto dto);
    Task DeleteEnrollmentAsync(int id);
}