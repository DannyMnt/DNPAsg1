using ClientBlazor.Components.Pages;
using DTOs;

namespace ClientBlazor.Services;

public interface ICommentService
{
    public Task<List<CreateCommentDto>> GetComments(int postId);

    public Task<CreateCommentDto> AddComment(CreateCommentDto request);
}