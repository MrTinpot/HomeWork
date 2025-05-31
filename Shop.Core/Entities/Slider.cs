using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Entities
{
    public class Slider : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Başlık"), Required(ErrorMessage = "{0} Boş Geçilemez!"), StringLength(150)]
        public string Title { get; set; }
        [Display(Name = "Açıklama"),StringLength(450)]
        public string Description { get; set; }
        [Display(Name = "Resim"), StringLength(100)]
        public string? Image { get; set; }
    }
}
