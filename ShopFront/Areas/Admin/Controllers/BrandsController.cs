using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Entities;
using ShopFront.Tools;

namespace ShopFront.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class BrandsController : Controller
    {
        string _apiUrl = "https://localhost:7068/api/brands/";
        HttpClient _httpClient = new HttpClient();
        // GET: BrandsController
        public async Task<ActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var model = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiUrl);
            return View(model);
        }

        // GET: BrandsController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var model = await _httpClient.GetFromJsonAsync<Brand>(_apiUrl + id);
            return View(model);
        }

        // GET: BrandsController/Create
        public ActionResult Create()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return View();
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Brand collection, IFormFile? Logo)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (Logo != null)
                    {
                        collection.Logo = FileHelper.FileLoader(Logo);
                        var response = await _httpClient.PostAsJsonAsync(_apiUrl, collection);
                        if (response.IsSuccessStatusCode)
                            return RedirectToAction(nameof(Index));
                        ModelState.AddModelError("", "Marka eklenirken hata oluştu.");
                    }
                    else
                    {
                        collection.Logo = "default.png"; // Varsayılan logo
                        var response = await _httpClient.PostAsJsonAsync(_apiUrl, collection);
                        if (response.IsSuccessStatusCode)
                            return RedirectToAction(nameof(Index));
                        ModelState.AddModelError("", "Marka eklenirken hata oluştu.");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Marka eklenirken hata oluştu.");
                }
            }
            return View(collection);
        }
        // GET: BrandsController/Edit/5
        public async  Task<ActionResult> EditAsync(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var model = await _httpClient.GetFromJsonAsync<Brand>(_apiUrl + id);
            return View(model);
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id,Brand collection,IFormFile? Logo,bool imageDel)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (Logo != null)
                    {
                        collection.Logo = FileHelper.FileLoader(Logo);
                    }
                    if (imageDel == true)
                    {
                        collection.Logo = string.Empty;
                        FileHelper.FileRemover(collection.Logo);
                        collection.Logo = "default.png"; // Varsayılan logo
                    }
                    var Response = await _httpClient.PutAsJsonAsync(_apiUrl + id, collection);
                    if (Response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ModelState.AddModelError("", "Marka Güncellenirken Hata Oluştu.");
                    ViewBag.Error = Response.StatusCode;
                }
                catch
                {
                    ModelState.AddModelError("", "Resim Silinirken Hata Oluştu.");
                }
            }
            return View(collection);
        }

        // GET: BrandsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var model = await _httpClient.GetFromJsonAsync<Brand>(_apiUrl + id);
            return View(model);
        }

        // POST: BrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Brand collection)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            try
            {
                var response = await _httpClient.DeleteAsync(_apiUrl+ id);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Silinirken Hata Oluştu!");
            }
            return View(collection);
        }
    }
}
