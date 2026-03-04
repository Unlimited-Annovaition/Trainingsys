using Application.DTOS.TrainerDto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services;

public class TrainerService : ITrainerService
{
    private  readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public TrainerService(IMapper mapper, IUnitOfWork uow)
    {
        _mapper = mapper;
        _uow = uow;
    }

    public async Task AddTrainerAsync(RegisterTrainerDTO trainer)
    {
        if (await _uow.Users.IsEmailExistsAsync(trainer.email))
        {
            throw new Exception("Email already exists");
        }
        var newUser = _mapper.Map<User>(trainer);
        newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(trainer.Password);
        newUser.RoleId = (int)Userroles.Trainer;
        int newuserId =  await _uow.Users.AddUserAsync(newUser);
        
        var newtrainer = _mapper.Map<Trainer>(trainer);
        newtrainer.UserId= newuserId;
        await _uow.Trainers.AddTrainerAsync(newtrainer);
        await  _uow.CompleteAsync();
    }

    public async Task<IEnumerable<TrainerResponseDTO>> GetTrainersAsync()
    {
        var trainers = await _uow.Trainers.GetAllTrainersAsync();
        return _mapper.Map<IEnumerable<TrainerResponseDTO>>(trainers);
    }
    
    
    public async Task<TrainerResponseDTO?> GetTrainerByIdAsync(int id)
    {
        var trainer = await _uow.Trainers.GetTrainerByIdAsync(id);
        
        if (trainer == null) 
            return null;
        
        return _mapper.Map<TrainerResponseDTO>(trainer);
    }
    
    public async Task UpdateTrainerAsync(int id, UpdateTrainerDTO dto)
    {
        var trainer = await _uow.Trainers.GetTrainerByIdAsync(id);
        
        if (trainer == null)
            throw new Exception("the trainer does not exist");

        trainer.FullName = dto.FullName;
        trainer.Specialization = dto.specialization;

        await _uow.Trainers.UpdateTrainerAsync(id, trainer);
        await _uow.CompleteAsync();
    }

    public async Task DeleteTrainerAsync(int id)
    {
        var trainer = await _uow.Trainers.GetTrainerByIdAsync(id);
        if (trainer == null)
            throw new Exception("the trainer does not exist");
        await _uow.Trainers.DeleteTrainerAsync(id);
        await _uow.CompleteAsync();
    }
}