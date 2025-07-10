using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Entities
{
    public class Orders : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Display(Name="Kullanıcı")]
        public User? User { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Ürün")]
        public Product? Product { get; set; }
        [Display(Name = "Adet")]
        public int Quantity { get; set; } = 1;
        [Display(Name = "Sipariş Tarihi")]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Display(Name = "Sipariş Durumu")]
        public string Status { get; set; } = "Bekleniyor";
        [Display(Name = "Sipariş Aktifklik")]
        public bool isActive { get; set; } = true;
    }
}
