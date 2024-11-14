using DTOs;

namespace ClientBlazor.Services;

public interface IUserService
{
    public Task<CreateUserDto> AddUserAsync(CreateUserDto request);
    public Task UpdateUserAsync(int id, CreateUserDto request);
    public Task<CreateUserDto> GetUserAsync(int id);
    public Task DeleteUserAsync(int id);
}