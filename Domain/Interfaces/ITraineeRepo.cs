using Domain.Entities;

namespace Domain.Interfaces;

public interface ITraineeRepo
{
    public Task<Trainee> GetTraineeAsync(int id);
    public Task<IEnumerable<Trainee>> GetAllTraineesAsync();
    public Task AddTraineeAsync(Trainee trainee);
    public Task UpdateTraineeAsync(Trainee trainee);
    public Task DeleteTraineeAsync(Trainee trainee);
    public Task<IEnumerable<Trainee>> GetTraineesByCourseIdAsync(int courseId); 
    public Task<IEnumerable<Trainee>> GetTraineesWithPaginationAsync(string searchTerm, int pageNumber, int pageSize); 
}