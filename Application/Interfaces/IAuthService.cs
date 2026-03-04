using Application.DTOS;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(LoginDTO dto);
}