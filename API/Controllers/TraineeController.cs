using Application.DTOs;
using Application.DTOS;
using Application.Interfaces;
using Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TraineeController : ControllerBase
{
    private readonly ITraineeServices _traineeService;
    
    public TraineeController(ITraineeServices traineeService)
    {
        _traineeService = traineeService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterTraineeDto dto)
    {
        await _traineeService.RegisterTraineeAsync(dto);
    
        var response = ApiResponse<object>.Ok(null, "Registration Successful");
        return Ok(response);
    }

    [HttpGet("getalltrainees")]
    [Authorize(Roles = "Trainee")] 
    public async Task<IActionResult> GetAll()
    {
        var trainees = await _traineeService.GetTraineesAsync();
        
        var response = ApiResponse<IEnumerable<TraineeResponseDto>>.Ok(trainees, "Retrieved Trainees");
        return Ok(response);
    }

    [HttpGet("GetTraineeByid/{id}")]
    [Authorize(Roles = "Trainee")] 
    public async Task<IActionResult> GetTraineeById(int id)
    {
        var trainee = await _traineeService.GetTraineeAsync(id);
        if (trainee == null)
        {
            var errorResponse = ApiResponse<object>.Fail("Trainee not found in the system.");
            return NotFound(errorResponse);
        }

        var response = ApiResponse<TraineeResponseDTO>.Ok(trainee, "Trainee data retrieved successfully.");
        return Ok(response);
    }

    [HttpPut("UpdateTrainee/{id}")]
    [Authorize(Roles = "Trainee")] 
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTraineeDto dto)
    {
        await _traineeService.UpdateTraineeAsync(id, dto);
    
        var response = ApiResponse<object>.Ok(null, "update trainee successful");
        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    [Authorize(Roles = "Trainee")] 
    public async Task<IActionResult> Delete(int id)
    {
        await _traineeService.DeleteTraineeAsync(id);
    
        var response = ApiResponse<object>.Ok(null, "the trainee deleted successfully");
        return Ok(response);
    }

    [HttpGet("paged")]
    [Authorize(Roles = "Trainee")] 
    public async Task<IActionResult> GetTraineesWithPagination(
        [FromQuery] string? searchTerm, 
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        var trainees = await _traineeService.GetTraineesWithPaginationAsync(searchTerm, pageNumber, pageSize);
        
        var response = ApiResponse<IEnumerable<TraineeResponseDto>>.Ok(trainees, "Trainees retrieved successfully with pagination.");
        return Ok(response);
    }
   
    [HttpGet("TraineesIncourse/{courseId}")]
    [Authorize(Roles = "Trainee")] 
    public async Task<IActionResult> GetTraineesByCourseId(int courseId)
    {
        var trainees = await _traineeService.GetTraineesByCourseIdAsync(courseId);
        
        if (!trainees.Any())
        {
            var emptyResponse = ApiResponse<IEnumerable<TraineeResponseDto>>.Ok(trainees, "No trainees found for this course.");
            return Ok(emptyResponse);
        }

        var response = ApiResponse<IEnumerable<TraineeResponseDto>>.Ok(trainees, "Trainees for the specified course retrieved successfully.");
        return Ok(response);
    }
}
    
