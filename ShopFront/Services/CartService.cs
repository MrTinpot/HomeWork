using Microsoft.AspNetCore.Http;
using System.Text.Json;
using ShopFront.Models;
namespace ShopFront.Services
{


public class CartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CartKey = "Cart";

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public List<CartItem> GetCart()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var cartJson = session.GetString(CartKey);
        return cartJson == null ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(cartJson)!;
    }

    public void SaveCart(List<CartItem> cart)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        session.SetString(CartKey, JsonSerializer.Serialize(cart));
    }

    public void AddToCart(CartItem item)
    {
        var cart = GetCart();
        var existing = cart.FirstOrDefault(x => x.ProductId == item.ProductId);
        if (existing != null)
            existing.Quantity += item.Quantity;
        else
            cart.Add(item);
        SaveCart(cart);
    }

    public void RemoveFromCart(int productId)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
        {
            cart.Remove(item);
            SaveCart(cart);
        }
    }

    public void ClearCart()
    {
        SaveCart(new List<CartItem>());
    }
}
}