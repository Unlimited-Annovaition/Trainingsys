using System.Data;
using Application.DTOS.TrainerDto;
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TrainerRepo : ITrainerRepo
{
    private readonly TrainingDbContext _Context;
    public TrainerRepo(TrainingDbContext context)
    {
        _Context = context;
    }

    public async Task<IEnumerable<Trainer>> GetAllTrainersAsync()
    {
        var connection = _Context.Database.GetDbConnection();
        var Trainers = await connection.QueryAsync<Trainer>(
            "sp_GetAllTrainersWithEmail",
            commandType:CommandType.StoredProcedure
        );
        return Trainers;
    }

    public async Task AddTrainerAsync(Trainer trainer)
    {
        var connection = _Context.Database.GetDbConnection();
        var parameters =  new DynamicParameters();
        parameters.Add("@FullName", trainer.FullName);
        parameters.Add("@Specialization", trainer.Specialization);
        parameters.Add("@UserId", trainer.UserId);
        await connection.ExecuteAsync("sp_InsertTrainer", parameters, commandType: CommandType.StoredProcedure);
    }
    public async Task DeleteTrainerAsync(int id)
    {
        var connection = _Context.Database.GetDbConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@TrainerId", id);
        await connection.ExecuteAsync("sp_DeleteTrainer", parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateTrainerAsync(int id, Trainer trainer)
    {
        var connection = _Context.Database.GetDbConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@TrainerId", id);
        parameters.Add("@FullName", trainer.FullName);
        parameters.Add("@Specialization", trainer.Specialization);
       await connection.ExecuteAsync("sp_UpdateTrainer", parameters, commandType: CommandType.StoredProcedure);
    }
    public async Task<Trainer> GetTrainerByIdAsync(int id)
    {
        var connection = _Context.Database.GetDbConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@TrainerId", id);
       var trainer = await connection.QueryFirstOrDefaultAsync<Trainer>("sp_GetTrainerByIdWithEmail", parameters, commandType: CommandType.StoredProcedure);
       return trainer;
    }
}