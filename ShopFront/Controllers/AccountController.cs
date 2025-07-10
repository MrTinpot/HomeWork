using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Entities;
using Shop.Core.Models;
using ShopFront.Models;


namespace ShopFront.Controllers
{
    public class AccountController : Controller
    {
        string _apiAdres = "https://localhost:7068/api/users/";
        private readonly HttpClient _httpClient;

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var users = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres);
            try
            {
                var userguid = User.FindFirst(ClaimTypes.UserData).Value;//FindFirst("UserData").Value;
                var user = users.FirstOrDefault(x => x.UserGuid.ToString() == userguid);
                if (userguid is null || user == null)
                {
                    await HttpContext.SignOutAsync();
                    return RedirectToAction("Login");
                }
                var orders = await _httpClient.GetFromJsonAsync<List<Orders>>("https://localhost:7068/api/orders/userId/" + user.Id);

                var viewModel = new AccountViewModel
                {
                    User = user,
                    Orders = orders ?? new List<Orders>()
                };
                return View(viewModel);
            }
            catch (Exception hata)
            {
                ModelState.AddModelError("", "Hata Oluştu! " + hata.Message);
                return View();
            }
        }
        [HttpPost]
        public IActionResult Index(User user)
        {
            try
            {
                // _dbContext.Users.Update(user);
                // _dbContext.SaveChanges();

                //_userService.Update(user);
                //_userService.Save();

                TempData["Message"] = @$"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Kayıt Başarılı!</strong> Üye Kaydınız Başarıyla Güncellenmiştir.
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div>";
            }
            catch (Exception hata)
            {
                ModelState.AddModelError("", "Hata Oluştu! " + hata.Message);
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUpAsync(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // var kullanici = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
                    var model = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres);

                    var kullanici = model.FirstOrDefault(x => x.Email == user.Email);

                    if (kullanici != null)
                    {
                        ModelState.AddModelError("", "Bu Email ile Daha Önce Kayıt Olunmuş!");
                    }
                    else
                    {
                        user.IsActive = true;
                        user.IsAdmin = false;
                        user.CreateDate = DateTime.Now;
                        //await _dbContext.Users.AddAsync(user);
                        //await _dbContext.SaveChangesAsync();
                        // await _userService.AddAsync(user);
                        // await _userService.SaveAsync();
                        TempData["Message"] = @$"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Kayıt Başarılı!</strong> Üye Kaydınız Başarıyla Tamamlanmıştır. Üye Girişi Yapabilirsiniz.
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div>";
                        return RedirectToAction("Login");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(user);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserLoginModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Call your Auth API to get the JWT token
                    var response = await _httpClient.PostAsJsonAsync("https://localhost:7068/api/Auth/Login", userLoginModel);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        dynamic tokenObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                        string accessToken = tokenObj.accessToken;

                        // Store JWT in session
                        HttpContext.Session.SetString("JWToken", accessToken);

                        // Optionally, get user info (you can decode the token or fetch user details)
                        // For now, you can keep your existing user lookup logic if needed

                        // Set up claims as before
                        // (You may want to get user info from the token or a user API)
                        var model = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres);

                        //var kullanici = await _dbContext.Users.FirstOrDefaultAsync(x => x.IsActive && x.Email == model.Email && x.Password == model.Password);
                        var kullanici = model.FirstOrDefault(x => x.IsActive && x.Email == userLoginModel.Email && x.Password == userLoginModel.Password);
                        if (kullanici != null)
                        {
                            var haklar = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, kullanici.Name),
                            new Claim(ClaimTypes.Email, kullanici.Email),
                            new Claim(ClaimTypes.UserData, kullanici.UserGuid.ToString()),
                            new Claim(ClaimTypes.Role, kullanici.IsAdmin ? "Admin" : "User"),
                            new Claim(ClaimTypes.NameIdentifier, kullanici.Id.ToString())
                        };
                            var kullaniciKimligi = new ClaimsIdentity(haklar, ClaimsIdentity.DefaultNameClaimType);
                            var claimsPrincipal = new ClaimsPrincipal(kullaniciKimligi);
                            await HttpContext.SignInAsync(claimsPrincipal); // yukarıdaki tüm ayarlarla beraber sisteme girişi yap
                            return Redirect(!string.IsNullOrEmpty(userLoginModel.ReturnUrl) ? userLoginModel.ReturnUrl : "/Anasayfa/Index");
                        }
                    }
                    else
                        ModelState.AddModelError("", "Giriş Başarısız!");
                }
                catch (Exception hata)
                {
                    ModelState.AddModelError("", "Hata Oluştu!" + hata.Message);
                }
            }
            return View(userLoginModel);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Anasayfa/Index");
        }
    }
}
