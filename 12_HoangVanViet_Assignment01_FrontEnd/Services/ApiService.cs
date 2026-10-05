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
        
        // --- System Account CRUD ---
        public async Task<SystemAccount> GetAccountByIdAsync(short id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}api/Accounts/{id}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<SystemAccount>();
            return null;
        }
        public async Task<bool> CreateAccountAsync(SystemAccount account)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}api/Accounts", account);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateAccountAsync(SystemAccount account)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}api/Accounts/{account.AccountID}", account);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateProfileAsync(SystemAccount account)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}api/Accounts/profile/{account.AccountID}", account);
            return response.IsSuccessStatusCode;
        }
        public async Task<string> DeleteAccountAsync(short id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}api/Accounts/{id}");
            if (response.IsSuccessStatusCode) return null; // Success
            var error = await response.Content.ReadAsStringAsync();
            return string.IsNullOrEmpty(error) ? "Failed to delete" : error; // Return error message
        }

        // --- Category CRUD ---
        public async Task<Category> GetCategoryByIdAsync(short id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}api/Categories/{id}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Category>();
            return null;
        }
        public async Task<bool> CreateCategoryAsync(Category category)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}api/Categories", category);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}api/Categories/{category.CategoryID}", category);
            return response.IsSuccessStatusCode;
        }
        public async Task<string> DeleteCategoryAsync(short id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}api/Categories/{id}");
            if (response.IsSuccessStatusCode) return null;
            var error = await response.Content.ReadAsStringAsync();
            return string.IsNullOrEmpty(error) ? "Failed to delete" : error;
        }

        // --- NewsArticle CRUD ---
        public async Task<List<NewsArticle>> GetNewsArticlesAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}api/NewsArticles");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<NewsArticle>>();
            return new List<NewsArticle>();
        }
        public async Task<List<NewsArticle>> GetActiveNewsArticlesAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}api/NewsArticles/active");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<NewsArticle>>();
            return new List<NewsArticle>();
        }
        public async Task<List<NewsArticle>> GetNewsHistoryAsync(short authorId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}api/NewsArticles/author/{authorId}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<NewsArticle>>();
            return new List<NewsArticle>();
        }
        public async Task<NewsArticle> GetNewsArticleByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}api/NewsArticles/{id}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<NewsArticle>();
            return null;
        }
        public async Task<bool> CreateNewsArticleAsync(NewsArticle article)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}api/NewsArticles", article);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateNewsArticleAsync(NewsArticle article)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}api/NewsArticles/{article.NewsArticleID}", article);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteNewsArticleAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}api/NewsArticles/{id}");
            return response.IsSuccessStatusCode;
        }

        // --- Reports ---
        public async Task<List<NewsArticle>> GetStatisticsAsync(System.DateTime startDate, System.DateTime endDate)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}api/Reports/statistics?startDate={startDate:O}&endDate={endDate:O}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<NewsArticle>>();
            return new List<NewsArticle>();
        }

        // --- Tags ---
        public async Task<List<Tag>> GetTagsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}api/Tags");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<Tag>>();
            return new List<Tag>();
        }
    }
}
