using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Kullanıcı Adı"),StringLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Display(Name = "Şifre"),StringLength(20, MinimumLength = 6, ErrorMessage = "{0} en az {1} karakter olmalıdır!"),DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Display(Name = "E-Posta"),Required(ErrorMessage = "{0} Boş Geçilemez!"),EmailAddress(ErrorMessage = "{0} Geçerli bir e-posta adresi olmalıdır!")]
        public string Email { get; set; } = string.Empty;
        [Display(Name = "Ad"), StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Soyad"), StringLength(50)]
        public string Surname { get; set; } = string.Empty;
        [Display(Name = "Telefon"), StringLength(15), DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; } = string.Empty;
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        [Display(Name = "Admin?")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)] 
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public Guid? UserGuid { get; set; } = Guid.NewGuid();

    }
}
