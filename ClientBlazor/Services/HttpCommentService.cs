using System.Text.Json;
using DTOs;
using Post = ClientBlazor.Components.Pages.Post;

namespace ClientBlazor.Services;

public class HttpCommentService: ICommentService
{
    private readonly HttpClient client;

    public HttpCommentService(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<List<CreateCommentDto>?> GetComments(int postId)
    {
        HttpResponseMessage httpResponseMessage = await client.GetAsync($"/Comment/post/{postId}");
        string response = await httpResponseMessage.Content.ReadAsStringAsync();
        if (!httpResponseMessage.IsSuccessStatusCode)
            throw new Exception(response);
        return JsonSerializer.Deserialize<List<CreateCommentDto>>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<CreateCommentDto?> GetCommentUsername(int id)
    {
        HttpResponseMessage httpResponseMessage = await client.GetAsync($"/Comment/user/{id}");
        string response = await httpResponseMessage.Content.ReadAsStringAsync();
        if (!httpResponseMessage.IsSuccessStatusCode)
            throw new Exception(response);
        return JsonSerializer.Deserialize<CreateCommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<CreateCommentDto?> AddComment(CreateCommentDto request)
    {
        HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync("/Comment", request);
        string response = await httpResponseMessage.Content.ReadAsStringAsync();
        if (!httpResponseMessage.IsSuccessStatusCode)
            throw new Exception(response);
        return JsonSerializer.Deserialize<CreateCommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });    
    }
}