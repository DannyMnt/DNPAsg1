using System.Security.Cryptography;

namespace DTOs;

public class CreatePostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public UserDto Author { get; set; }
    public List<CreateCommentDto> Comments { get; set; }
}