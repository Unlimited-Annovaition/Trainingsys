using System.Diagnostics.Contracts;
using Domain.Entities;

namespace Domain.Interfaces;

public interface ITrainerRepo
{
    Task<IEnumerable<Trainer>> GetAllTrainersAsync();
    Task<Trainer?> GetTrainerByIdAsync(int id);
    Task AddTrainerAsync(Trainer trainer);
    Task UpdateTrainerAsync(int Id,Trainer trainer);
    Task DeleteTrainerAsync(int id);
}