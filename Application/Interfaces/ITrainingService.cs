using Application.DTOS.TrainerDto;
using Domain.Entities;

namespace Application.Interfaces;

public interface ITrainerService
{
    Task AddTrainerAsync(RegisterTrainerDTO trainer);
    Task<IEnumerable<TrainerResponseDTO>> GetTrainersAsync();
    Task<TrainerResponseDTO> GetTrainerByIdAsync(int id);
    Task UpdateTrainerAsync(int id,UpdateTrainerDTO trainer);
    Task DeleteTrainerAsync(int id);
}