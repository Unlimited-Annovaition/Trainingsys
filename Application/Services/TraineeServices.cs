using Application.DTOs;
using Application.DTOS;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services;

public class TraineeServices : ITraineeServices
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    public TraineeServices(IUnitOfWork unitOfWork , IMapper mapper)
    {
        _uow = unitOfWork;
        _mapper = mapper;
    }
    public async Task<bool> RegisterTraineeAsync(RegisterTraineeDto registerTraineeDto)
    {
        if (await _uow.Users.IsEmailExistsAsync(registerTraineeDto.Email))
        {
            throw new Exception("Email already exists");
        }
        var newUser = _mapper.Map<User>(registerTraineeDto);
        newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerTraineeDto.Password);
        newUser.RoleId = (int)Userroles.Trainee;
        
        int newUserID = await _uow.Users.AddUserAsync(newUser);

        var newTrainee = _mapper.Map<Trainee>(registerTraineeDto);
        newTrainee.UserId = newUserID;
        await _uow.Trainees.AddTraineeAsync(newTrainee);
        await _uow.CompleteAsync();
        return true;
    }

    public async Task<IEnumerable<TraineeResponseDto>> GetTraineesAsync()
    {
        var trainees = await _uow.Trainees.GetAllTraineesAsync();
        return _mapper.Map<IEnumerable<TraineeResponseDto>>(trainees);
    }

    public async Task<TraineeResponseDTO> GetTraineeAsync(int id)
    {
        var trainee =await _uow.Trainees.GetTraineeAsync(id);
        return _mapper.Map<TraineeResponseDTO>(trainee);
    }

    public async Task UpdateTraineeAsync(int id,UpdateTraineeDto updateTraineeDto)
    {
        var trainee = await _uow.Trainees.GetTraineeAsync(id);
        if (trainee == null) 
            throw new Exception("the trainee does not exist");

        trainee.Fullname = updateTraineeDto.FullName;
        trainee.PhoneNumber = updateTraineeDto.PhoneNumber;
        await _uow.Trainees.UpdateTraineeAsync(trainee);
        await _uow.CompleteAsync();
    }
    
    public async Task DeleteTraineeAsync(int id)
    {
        var trainee = await _uow.Trainees.GetTraineeAsync(id);
        if (trainee == null) 
            throw new Exception("the trainee does not exist");
        
        await _uow.Trainees.DeleteTraineeAsync(trainee);
        await _uow.CompleteAsync();
    }

    public async Task<IEnumerable<TraineeResponseDto>> GetTraineesWithPaginationAsync(string? searchTerm, int pageNumber, int pageSize)
    {
        var trainees = await _uow.Trainees.GetTraineesWithPaginationAsync(searchTerm, pageNumber, pageSize);
        return _mapper.Map<IEnumerable<TraineeResponseDto>>(trainees);
    }

    public async Task<IEnumerable<TraineeResponseDto>> GetTraineesByCourseIdAsync(int courseId)
    {
        var trainees = await _uow.Trainees.GetTraineesByCourseIdAsync(courseId);
        return _mapper.Map<IEnumerable<TraineeResponseDto>>(trainees);
    }
}