using Shop.Core.Entities;

namespace ShopFront.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<Slider>? Sliders { get; set; }
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<Category>? Categories { get; set; } 
    }
}
