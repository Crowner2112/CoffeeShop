using Models.EF;
using System;

namespace CoffeeShop.Models
{
    [Serializable]
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}