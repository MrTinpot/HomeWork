using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Entities
{
    public class Brand : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Marka Adı"),Required(ErrorMessage ="Marka Adı Girmek Zorunludur!")]
        public string Name { get; set; }
        [Display(Name = "Marka Açıklaması"),DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        [Display(Name = "Marka Logosu")]
        public string? Logo { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Durumu")]
        public bool IsActive { get; set; }
        public IList<Product>? Products { get; set; }
    }
}
