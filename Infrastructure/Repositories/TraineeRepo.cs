using System.Data;
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TraineeRepo : ITraineeRepo
{
    private readonly TrainingDbContext _context;
    public TraineeRepo(TrainingDbContext  context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Trainee>> GetAllTraineesAsync()
    {
        return await _context.Trainees
            .Include(t => t.User) 
            .ToListAsync();
    }

    public async Task<Trainee> GetTraineeAsync(int id)
    {
        var result = await _context.Trainees.FromSqlRaw("EXEC sp_GetTraineeById @TraineeId ={0}", id)
            .ToListAsync();
        return result.FirstOrDefault();
    }
    
    public async Task AddTraineeAsync(Trainee trainee)
    {
        await _context.Database.ExecuteSqlRawAsync("EXEC sp_InsertTrainee @FullName = {0},@PhoneNumber = {1},@UserId = {2}", trainee.Fullname, trainee.PhoneNumber, trainee.UserId);
    }

    public async Task UpdateTraineeAsync(Trainee trainee)
    {
        await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateTrainee @TraineeId = {0},@FullName = {1},@PhoneNumber = {2}",
        trainee.TraineeId,trainee.Fullname,trainee.PhoneNumber);
    }
    
    public async Task DeleteTraineeAsync(Trainee trainee)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_DeleteTrainee @TraineeId = {0}", 
            trainee.TraineeId);
    }
    

    public async Task<bool> IsEmailExistsAsync(string email)
    {
        var result = await _context.Database
            .SqlQueryRaw<int>("EXEC sp_CheckEmailExists @Email = {0}", email)
            .ToListAsync();
            
        return result.FirstOrDefault() == 1;
    }
    public async Task<IEnumerable<Trainee>> GetTraineesByCourseIdAsync(int courseId)
    {
       var connection = _context.Database.GetDbConnection();
       var para =  new DynamicParameters();
       para.Add("@CourseId", courseId);
       var trainees = await connection.QueryAsync<Trainee>(
        "sp_GetTraineesByCourseId",
        para,
        commandType: CommandType.StoredProcedure
       );
       return trainees;
    }

    public async Task<IEnumerable<Trainee>> GetTraineesWithPaginationAsync(string searchTerm, int pageNumber, int pageSize)
    {
        var connection = _context.Database.GetDbConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@SearchItem", searchTerm);
        parameters.Add("@pageNumber", pageNumber);
        parameters.Add("@pageSize", pageSize);
        var trainees = await connection.QueryAsync<Trainee>(
            "sp_GetTrainees_SearchAndPaginate",
            parameters,
            commandType: CommandType.StoredProcedure
        );
        return trainees;
    }
    
}