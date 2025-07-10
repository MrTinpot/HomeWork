using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Ürün Adı"), Required(ErrorMessage = "{0} Boş Geçilemez!"), StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Ürün Açıklaması"), DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        [Column("Image",TypeName ="Varchar")]
        [Display(Name = "Ürün Resmi"), StringLength(50)]
        public string? Image { get; set; }
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }
        [Display(Name = "Ürün Stok")]
        public int Stock { get; set; }
        [Display(Name = "Kayıt Tarihi"),ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Durumu")]
        public bool IsActive { get; set; }
        [Display(Name = "AnaSayfa Ürünü")]
        public bool IsHome { get; set; }
        [Display(Name = "Ürün Kategorisi"),Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public int CategoryId { get; set; }
        [Display(Name = "Kategori")]
        public Category? Category { get; set; }

        [Display(Name = "Ürün Markası"),Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public int BrandId { get; set; }
        [Display(Name = "Marka")]
        public Brand? Brand { get; set; }
        [Display(Name = "İndirimli Ürün")]
        public bool Sale { get; set; } = false; // İndirimli ürünler için kullanılabilir, varsayılan olarak false
        [Display(Name = "İndirim Yüzdesi")]
        public int SalePercentage { get; set; } = 1; // İndirim yüzdesi, varsayılan olarak 1
    }
}
