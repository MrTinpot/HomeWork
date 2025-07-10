using Microsoft.AspNetCore.Mvc;
using Shop.Core.Entities;

namespace ShopFront.Controllers
{
    public class CategoriesController : Controller
    {
        string _apiAdres = "https://localhost:7068/api/";
        private readonly HttpClient _httpClient;

        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
                return BadRequest("Id Bilgisi Gereklidir!");

            var category = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + "Categories/" + id);
            if (category == null)
                return NotFound();

            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "Products");
            var allCategories = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres + "Categories");

            List<Product> displayProducts = new List<Product>();

            if (category.ParentId == 0)
            {
                // Parent: get products from itself and all child categories
                var childCategoryIds = allCategories.Where(c => c.ParentId == id && c.IsActive).Select(c => c.Id).ToList();
                displayProducts.AddRange(products.Where(p => p.IsActive && (p.CategoryId == id.Value || childCategoryIds.Contains(p.CategoryId))));
            }
            else
            {
                // Child: only show products from this category
                displayProducts.AddRange(products.Where(p => p.IsActive && p.CategoryId == id.Value));
            }

            category.Products = displayProducts;

            // Optionally, pass child categories for navigation
            ViewBag.ChildCategories = allCategories.Where(c => c.ParentId == id && c.IsActive).ToList();

            return View(category);
        }
    }
}
