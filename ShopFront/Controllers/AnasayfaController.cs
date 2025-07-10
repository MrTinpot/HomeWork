using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Shop.Core.Entities;
using ShopFront.Models;

namespace ShopFront.Controllers
{
    public class AnasayfaController : Controller
    {
        private readonly ILogger<AnasayfaController> _logger;
        string _apiAdres = "https://localhost:7068/api/";
        private readonly HttpClient _httpClient;
        public AnasayfaController(ILogger<AnasayfaController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _httpClient.GetFromJsonAsync<List<Slider>>(_apiAdres + "Sliders");
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "Products");
            var categories = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres + "Categories");
            var model = new HomePageViewModel
            {
                Sliders = sliders,
                Products = products,
                Categories = categories
            };
            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactAsync(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync(_apiAdres + "Contacts", contact);
                    if (response.IsSuccessStatusCode)
                    {
                        //await MailHelper.SendMailAsync(contact);
                        TempData["Message"] = @$"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Ýþlem Baþarýlý!</strong> Mesajýnýz Ýletilmiþtir.
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div>";
                        return RedirectToAction("Contact");
                    }
                    else
                        ModelState.AddModelError("", "Kayýt Baþarýsýz!");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Oluþtu!");
                }
            }
            return View(contact);
        }

    }
}
