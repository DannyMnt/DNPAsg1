using System.Text.Json;
using DTOs;

namespace ClientBlazor.Services;

public class HttpUserService: IUserService
{
    private readonly HttpClient client;
    private readonly Dictionary<int, string> usernames = new();
    public HttpUserService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<string> GetUsername(int userId)
    {
        if (usernames.ContainsKey(userId))
            return usernames[userId];

        var user = await GetUserAsync(userId);
        string username = user?.Username ?? "Unknown";
        usernames[userId] = username;
        return username;
    }
    
    public async Task LoadUsernamesForPosts(List<CreatePostDto> posts)
    {
        foreach (var post in posts)
        {
            await GetUsername(post.UserId);
        }
    }

    public async Task LoadUsernamesForComments(List<CreateCommentDto> comments)
    {
        foreach (var comment in comments)
        {
            await GetUsername(comment.UserId);
        }
    }
    
    public async Task<CreateUserDto?> AddUserAsync(CreateUserDto request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("/User", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if(!httpResponse.IsSuccessStatusCode)
            throw new Exception(response);
        return JsonSerializer.Deserialize<CreateUserDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task UpdateUserAsync(int id, CreateUserDto request)
    {
        HttpResponseMessage httpResponse = await client.PutAsJsonAsync($"User/{id}", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if(!httpResponse.IsSuccessStatusCode)
            throw new Exception(response);
    }
    
    public async Task<CreateUserDto?> GetUserAsync(int id)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"User/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
            throw new Exception($"Error: {httpResponse.StatusCode}, Response: {response}");

        return JsonSerializer.Deserialize<CreateUserDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task DeleteUserAsync(int id)
    {
        HttpResponseMessage httpResponse = await client.DeleteAsync($"User/{id}");
        if (!httpResponse.IsSuccessStatusCode)
        {
            string response = await httpResponse.Content.ReadAsStringAsync();
            throw new Exception(response);
        }
    }
}