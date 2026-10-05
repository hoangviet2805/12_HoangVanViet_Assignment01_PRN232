using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using _12_HoangVanViet_Assignment01_FrontEnd.Models;
using System.Text.Json;

namespace _12_HoangVanViet_Assignment01_FrontEnd.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<SystemAccount> LoginAsync(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}api/Auth/login", new { Email = email, Password = password });
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SystemAccount>();
            }
            return null;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}odata/Categories");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(content);
                if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    return jsonDoc.RootElement.Deserialize<List<Category>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else if (jsonDoc.RootElement.TryGetProperty("value", out var valueProp))
                {
                    return valueProp.Deserialize<List<Category>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            return new List<Category>();
        }
        public async Task<List<SystemAccount>> GetSystemAccountsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}odata/Accounts");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(content);
                if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    return jsonDoc.RootElement.Deserialize<List<SystemAccount>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else if (jsonDoc.RootElement.TryGetProperty("value", out var valueProp))
                {
                    return valueProp.Deserialize<List<SystemAccount>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            return new List<SystemAccount>();
        }
        
        // Add more methods for CRUD as needed
    }
}
