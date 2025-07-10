using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopFront.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class JsCategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
