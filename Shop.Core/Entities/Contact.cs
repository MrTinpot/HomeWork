using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Entities
{
    public class Contact : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Adınız"),Required(ErrorMessage = "{0} Boş Geçilemez!"),StringLength(30)]
        public string Name { get; set; }
        [Display(Name = "Soyadınız"), Required(ErrorMessage = "{0} Boş Geçilemez!"), StringLength(30)]
        public string Surname { get; set; }
        [Display(Name = "E-Posta Adresiniz"), Required(ErrorMessage = "{0} Boş Geçilemez!"), EmailAddress(ErrorMessage = "Geçerli bir {0} giriniz!")]
        public string Email { get; set; }
        [Display(Name = "Telefon Numaranız"), StringLength(16)]
        public string? Phone { get; set; }
        [Display(Name = "Mesajınız"), Required(ErrorMessage = "{0} Boş Geçilemez!"), DataType(DataType.MultilineText), StringLength(500, ErrorMessage = "{0} en fazla {1} karakter olabilir!")]
        public string Message { get; set; }
        [Display(Name = "Gönderi Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
