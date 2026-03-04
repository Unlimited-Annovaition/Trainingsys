using Application.DTOs;
using Application.DTOS;
using Domain.Entities;

namespace Application.Interfaces;

public interface ITraineeServices
{
    Task<bool>RegisterTraineeAsync(RegisterTraineeDto registerTraineeDto);
    Task<TraineeResponseDTO> GetTraineeAsync(int id);
    Task<IEnumerable<TraineeResponseDto>> GetTraineesAsync();
    Task UpdateTraineeAsync (int id,UpdateTraineeDto trainee);
    Task DeleteTraineeAsync(int id);
    Task<IEnumerable<TraineeResponseDto>> GetTraineesWithPaginationAsync(string? searchTerm, int pageNumber, int pageSize);
    Task<IEnumerable<TraineeResponseDto>> GetTraineesByCourseIdAsync(int courseId);
}