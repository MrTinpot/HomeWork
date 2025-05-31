using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Kategori Adı"), StringLength(50), Required(ErrorMessage = "Kategori Adı Zorunludur!")]
        public string Name { get; set; }
        [Display(Name = "Kategori Açıklaması"), DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        [Display(Name = "Kategori Resmi"), StringLength(50)]
        public string? Image { get; set; }
        [Display(Name = "Eklenme Tarihi"),ScaffoldColumn(false)] //Sccafold collumn false View'da gösterilmez
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Durumu")]
        public bool IsActive { get; set; }
        [Display(Name = "Üst Menüde Göster")]
        public bool IsTopMenu { get; set; }
        [Display(Name = "Üst Kategori")]
        public int? ParentId { get; set; } //null olabilir çünkü üst kategori olmayabilir
        public IList<Product>? Products { get; set; }
    }
}
