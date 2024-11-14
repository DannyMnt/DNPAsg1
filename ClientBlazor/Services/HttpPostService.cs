using System.Text.Json;
using DTOs;

namespace ClientBlazor.Services;

public class HttpPostService : IPostService
{
    private readonly HttpClient client;

    public HttpPostService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<CreatePostDto?> AddPostAsync(CreatePostDto request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("/Post", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
            throw new Exception(response);
        return JsonSerializer.Deserialize<CreatePostDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task UpdatePostAsync(int id, CreatePostDto request)
    {
        HttpResponseMessage httpResponse = await client.PutAsJsonAsync($"Post/{id}", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
            throw new Exception(response);
    }

    public async Task<CreatePostDto?> GetPostByIdAsync(int id)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"Post/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
            throw new Exception($"Error: {httpResponse.StatusCode}, Response: {response}");

        return JsonSerializer.Deserialize<CreatePostDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<List<CreatePostDto>?> GetPostsAsync()
    {
        HttpResponseMessage httpResponse = await client.GetAsync("Post/posts");
        List<CreatePostDto> posts = new List<CreatePostDto>();
        string response = await httpResponse.Content.ReadAsStringAsync();
        if(!httpResponse.IsSuccessStatusCode)
            throw new Exception($"Error: {httpResponse.StatusCode}, Response: {response}");
        
        return JsonSerializer.Deserialize<List<CreatePostDto>>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task DeletePostAsync(int id)
    {
        HttpResponseMessage httpResponse = await client.DeleteAsync($"Post/{id}");
        if (!httpResponse.IsSuccessStatusCode)
        {
            string response = await httpResponse.Content.ReadAsStringAsync();
            throw new Exception(response);
        }
    }
}