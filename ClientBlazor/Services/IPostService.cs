using DTOs;

namespace ClientBlazor.Services;

public interface IPostService
{
    public Task<CreatePostDto> AddPostAsync(CreatePostDto request);
    public Task UpdatePostAsync(int id, CreatePostDto request);
    public Task<CreatePostDto> GetPostByIdAsync(int id);
    public Task<List<CreatePostDto>?> GetPostsAsync();
    public Task DeletePostAsync(int id);
}