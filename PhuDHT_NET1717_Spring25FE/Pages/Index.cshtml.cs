using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhuDHT_NET1717_Spring25FE.Pages.Model;
using PhuDHT_NET1717_Spring25FE.Service;

namespace PhuDHT_NET1717_Spring25FE.Pages;

public class IndexModel : PageModel
{
    private readonly ApiService _apiService;

    public IndexModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public List<Order> Orders { get; set; } = new List<Order>();

    public async Task OnGet()
    {
        try
        {
            var response = await _apiService.GetOrdersAsync();
            if (response != null && response.Status == 1)
            {
                Orders = new List<Order>(response.Data);
            }
            else
            {
                Console.WriteLine("API trả về status khác 1");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi gọi API: {ex.Message}");
        }
    }
}