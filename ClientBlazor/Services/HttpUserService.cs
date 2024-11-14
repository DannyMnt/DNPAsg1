using System.Text.Json;
using DTOs;

namespace ClientBlazor.Services;

public class HttpUserService: IUserService
{
    private readonly HttpClient client;

    public HttpUserService(HttpClient client)
    {
        this.client = client;
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