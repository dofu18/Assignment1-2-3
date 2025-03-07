using System.Text.Json;
using PhuDHT_NET1717_Spring25FE.Pages.Model;

namespace PhuDHT_NET1717_Spring25FE.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<Order[]>> GetOrdersAsync()
        {
            var response = await _httpClient.GetAsync("https://your-api-url/api/order");
            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Response Status: {response.StatusCode}");
            Console.WriteLine($"Response Body: {responseBody}");

            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<ApiResponse<Order[]>>(responseBody, options);
        }
    }
}
