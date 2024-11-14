using DTOs;

namespace ClientBlazor.Services;

public interface IUserService
{
    public Task<string> GetUsername(int userId);
    public Task LoadUsernamesForComments(List<CreateCommentDto> comments);
    public Task LoadUsernamesForPosts(List<CreatePostDto> posts);
    public Task<CreateUserDto> AddUserAsync(CreateUserDto request);
    public Task UpdateUserAsync(int id, CreateUserDto request);
    public Task<CreateUserDto> GetUserAsync(int id);
    public Task DeleteUserAsync(int id);
}