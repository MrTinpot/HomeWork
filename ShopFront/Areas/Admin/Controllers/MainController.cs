using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Entities;
using ShopFront.Areas.Admin.Models;
namespace ShopFront.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class MainController : Controller
    {
        string _apiUrl = "https://localhost:7068/api/";
        HttpClient _httpClient = new HttpClient();
        public async Task<ActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var orders = await _httpClient.GetFromJsonAsync<List<Orders>>(_apiUrl + "Orders");
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiUrl + "Products");
            var brands = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiUrl + "Brands");
            var users = await _httpClient.GetFromJsonAsync<List<User>>(_apiUrl + "Users");
            var model = new HomePageViewModel
            {
                Users = users,
                Brands = brands,
                Orders = orders,
                Products = products,
                TotalOrders = orders?.Count ?? 0,
                TotalProducts = products?.Count ?? 0,
                TotalBrands = brands?.Count ?? 0,
                TotalUsers = users?.Count ?? 0
            };
            return View(model);
        }
    }
}
