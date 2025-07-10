using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Models
{
    public class UserLoginModel
    {
        [StringLength(50), Required(ErrorMessage = "{0} Alanı Zorunludur!"), EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Şifre"), StringLength(50), Required(ErrorMessage = "{0} Alanı Zorunludur!"), DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Beni Hatırla")]
        public string? ReturnUrl { get; set; }
    }
}
