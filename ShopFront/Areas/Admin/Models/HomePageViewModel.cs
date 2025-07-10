using Microsoft.AspNetCore.Mvc;
using Shop.Core.Entities;

namespace ShopFront.Areas.Admin.Models
{
    [Area("Admin")]
    public class HomePageViewModel
    {
        public IEnumerable<User>? Users { get; set; }
        public IEnumerable<Brand>? Brands { get; set; }
        public IEnumerable<Orders>? Orders { get; set; }
        public IEnumerable<Product>? Products { get; set; }
        public int TotalOrders { get; set; }
        public int TotalProducts { get; set; }
        public int TotalUsers { get; set; }
        public int TotalBrands { get; set; }
    }
}
