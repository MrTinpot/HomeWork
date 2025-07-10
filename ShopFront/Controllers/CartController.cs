using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Shop.Core.Entities;
using Shop.Service;
using ShopFront.Controllers;
using ShopFront.Models;
using ShopFront.Services;

public class CartController : Controller
{
    private readonly CartService _cartService;
    string _apiAdres = "https://localhost:7068/api/";
    private readonly HttpClient _httpClient;


    public CartController(CartService cartService, IHttpClientFactory httpClientFactory)
    {
        _cartService = cartService;
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpPost]
    public async Task<IActionResult> Add(int productId, int quantity)
    {
        var product = await _httpClient.GetFromJsonAsync<Product>(_apiAdres + "products/" + productId);
        _cartService.AddToCart(new CartItem
        {
            ProductId = product.Id,
            ProductName = product.Name,
            Price = product.Price,
            Quantity = quantity,
            Image = product.Image,
        });
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AddAjax(int productId, int quantity)
    {

        var product = await _httpClient.GetFromJsonAsync<Product>(_apiAdres + "products/" + productId);
        _cartService.AddToCart(new CartItem
        {
            ProductId = product.Id,
            ProductName = product.Name,
            ProductStock = product.Stock,
            Price = product.Price,
            Quantity = quantity,
            Image = product.Image,
        });
        var cartCount = _cartService.GetCart().Sum(x => x.Quantity);
        return Json(new { success = true, cartCount });
    }

    public IActionResult Index()
    {
        var cart = _cartService.GetCart();
        return View(cart);
    }

    [HttpPost]
    public IActionResult Remove(int productId)
    {
        _cartService.RemoveFromCart(productId);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Clear()
    {
        _cartService.ClearCart();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Order()
    {
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var cart = _cartService.GetCart();
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userId))
                return RedirectToAction("Login", "Account");

            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            foreach (var item in cart)
            {
                var order = new Orders
                {
                    UserId = userId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    OrderDate = DateTime.Now,
                    Status = "Bekleniyor",
                    isActive = true
                };

                var response = await _httpClient.PostAsJsonAsync(_apiAdres + "orders/", order);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Sipariþ oluþturulurken hata oluþtu.");
                    return View("Index", cart);
                }
            }

            _cartService.ClearCart();
            return RedirectToAction("Index", "Anasayfa");
        }
    }
}