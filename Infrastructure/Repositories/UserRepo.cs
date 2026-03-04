using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepo : IUserRepo
{
    private readonly TrainingDbContext _context;
    public UserRepo(TrainingDbContext context)
    {
        _context = context;
    }
    public async Task<int> AddUserAsync(User user)
    {
        var result = await _context.Database
            .SqlQueryRaw<int>("EXEC sp_InsertUser @FirstName = {0},  @Email = {1}, @PasswordHash = {2}, @RoleId = {3}", 
               user.UserName, user.Email, user.PasswordHash, user.RoleId)
            .ToListAsync();
        return result.FirstOrDefault(); 
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users.FromSqlRaw("EXEC sp_GetUserByEmail @Email = {0}", email)
            .ToListAsync();
        return user.FirstOrDefault();
    }
    public async Task<bool> IsEmailExistsAsync(string email)
    {
        var result = await _context.Database
            .SqlQueryRaw<int>("EXEC sp_CheckEmailExists @Email = {0}", email)
            .ToListAsync();
            
        return result.FirstOrDefault() == 1;
    }
}