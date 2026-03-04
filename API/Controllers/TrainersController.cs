using Application.DTOS.TrainerDto;
using Application.Interfaces;
using Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/trainers")]
public class TrainersController : ControllerBase
{
    private readonly ITrainerService _trainerServices;
    
    public TrainersController(ITrainerService traineeServices)
    {
        _trainerServices = traineeServices;
    }

    [HttpPost("registerTrainer")]
    public async Task<IActionResult> RegisterTrainer([FromBody]RegisterTrainerDTO trainer)
    {
        await _trainerServices.AddTrainerAsync(trainer);
        
        var response = ApiResponse<object>.Ok(null, "Trainer added successfully.");
        return Ok(response);
    }

    [HttpGet]
    [Authorize("Trainee")]
    public async Task<IActionResult> GetAllTrainers()
    {
        var trainers = await _trainerServices.GetTrainersAsync();
        
        var response = ApiResponse<IEnumerable<TrainerResponseDTO>>.Ok(trainers, "Trainers retrieved successfully.");
        return Ok(response);
    }

    [HttpGet( "getTrainer/{id}")]
    [Authorize("Trainee")]
    public async Task<IActionResult> GetTrainerById(int id)
    {
        var trainer = await _trainerServices.GetTrainerByIdAsync(id);
            
        if (trainer == null)
        {
            return NotFound(ApiResponse<object>.Fail("Trainer not found in the system."));
        }

        var response = ApiResponse<TrainerResponseDTO>.Ok(trainer, "Trainer details retrieved successfully.");
        return Ok(response);
    }
    
    [HttpPut("UpdateTrainer/{id}")]
    public async Task<IActionResult> UpdateTrainer(int id, [FromBody] UpdateTrainerDTO dto)
    {
        await _trainerServices.UpdateTrainerAsync(id, dto);
            
        var response = ApiResponse<object>.Ok(null, "Trainer data updated successfully.");
        return Ok(response);
    }

    [HttpDelete("DeleteTrainer/{id}")]
    public async Task<IActionResult> DeleteTrainer(int id)
    {
        await _trainerServices.DeleteTrainerAsync(id);
    
        var response = ApiResponse<object>.Ok(null, "Trainer deleted successfully.");
        return Ok(response);
    }
}