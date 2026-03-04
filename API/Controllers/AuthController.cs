using Application.DTOS;
using Application.Interfaces;
using Application.Wrappers; 
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        try
        {
            string token = await _authService.LoginAsync(dto);
            
            var response = ApiResponse<string>.Ok(token, "logged in");
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = ApiResponse<string>.Fail(ex.Message);
            return BadRequest(errorResponse);
        }
    }
}