using DTOs;
using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userController;

    public AuthController(IUserRepository userController)
    {
        this.userController = userController;
    }

    [HttpPost("auth/login")]
    public async Task<IResult> Login([FromBody] CreateUserDto user)
    {
        UserDto userDto;
        var returnedUser = await userController.getSingleAsync(user.Username);
        if (returnedUser != null)
        {
            if (returnedUser.Password == user.Password)
            {
                userDto = new UserDto()
                {
                    Id = returnedUser.Id,
                    Username = user.Username
                };
                return Results.Ok(userDto);
            }
        }

        return Results.Unauthorized();
    }
}