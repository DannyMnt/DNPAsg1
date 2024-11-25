using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using IQueryable = System.Linq.IQueryable;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]

public class PostController : ControllerBase
{
    private readonly IPostRepository postRepository;

    public PostController(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    [HttpPost]
    public async Task<ActionResult<CreatePostDto>> CreatePost([FromBody] CreatePostDto req)
    {
        Post post = new Post(req.Title, req.Body, req.UserId);
        Post created = await postRepository.AddAsync(post);
        CreatePostDto dto = new()
        {
            Id = created.Id,
            Title = created.Title,
            Body = created.Body,
            UserId = created.UserId
        };
        return Created($"posts/{created.Id}", dto);
    }

    [HttpPut("{id}")]
    public async Task<IResult> ReplacePost([FromRoute] int id, [FromBody] CreatePostDto req, [FromServices] IPostRepository postRepository)
    {
        Post existingPost = await postRepository.getSingleAsync(id);
        existingPost.Title = req.Title;
        existingPost.Body = req.Body;
        existingPost.UserId = req.UserId;
        
        await postRepository.UpdateAsync(existingPost);
        return Results.Ok();
    }
      
    [HttpGet("id/{id}")]
    public async Task<IResult> GetPost([FromRoute] int id, [FromQuery] bool includeAuthor, [FromQuery] bool includeComments)
    {
        var query = ((IQueryable<Post>)postRepository.getMany())
            .Where(p => p.Id == id);

        if (includeAuthor)
            query = query.Include(p => p.User);
        if (includeComments)
            query = query.Include(p => p.Comments);

        var post = await query.FirstOrDefaultAsync();

        if (post == null)
            return Results.NotFound();

        var dto = new CreatePostDto
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
            UserId = post.UserId,
            Author = includeAuthor ? new UserDto
            {
                Id = post.User.Id,
                Username = post.User.Username
            } : null,
            Comments = includeComments 
                ? post.Comments.Select(c => new CreateCommentDto
                {
                    Body = c.Body,
                    PostId = c.PostId,
                    UserId = post.UserId
                }).ToList() 
                : new List<CreateCommentDto>()
        };

        return Results.Ok(dto);
    }

    [HttpGet]
    public async Task<IResult> GetPostsByTitle([FromQuery] string? title)
    {
        var posts = (IQueryable<Post>)postRepository.getMany();
        if (!string.IsNullOrEmpty(title))
        {
            posts = posts.Where(t => t.Title.ToLower().Contains(title.ToLower()));
        }
        return Results.Ok(posts);
    }

    [HttpGet("posts")]
    public async Task<IQueryable> GetPosts()
    {
        IQueryable posts = postRepository.getMany();
        return posts;
    }

    [HttpGet("user/{userId}")]
    public async Task<IResult> GetPostsByUser([FromRoute] int? userId)
    {
        var posts = (IQueryable<Post>)postRepository.getMany();
        posts = posts.Where(t => t.UserId == userId);
        return Results.Ok(posts);
    }

    [HttpDelete("{id:int}")]
    public async Task<IResult> DeletePost([FromRoute] int id)
    {
        await postRepository.DeleteAsync(id);
        return Results.NoContent();
    }
}