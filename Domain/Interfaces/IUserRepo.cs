using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepo
{
    public Task<int> AddUserAsync(User user);
    public Task<bool> IsEmailExistsAsync(string email);
    public Task<User> GetUserByEmailAsync(string email);
}