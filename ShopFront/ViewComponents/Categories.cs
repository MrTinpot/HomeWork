using Microsoft.AspNetCore.Mvc;
using Shop.Core.Entities;

namespace ShopFront.ViewComponents
{
    public class Categories : ViewComponent
    {
        string _apiAdres = "https://localhost:7068/api/Categories/";
        private readonly HttpClient _httpClient;

        public Categories(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
            return View(model);
        }
    }
}
