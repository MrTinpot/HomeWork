using Shop.Core.Entities;
using System.Collections.Generic;

namespace ShopFront.Models
{
    public class AccountViewModel
    {
        public User User { get; set; }
        public List<Orders> Orders { get; set; }
    }
}