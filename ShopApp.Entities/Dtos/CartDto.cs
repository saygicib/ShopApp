using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Entities.Dtos
{
    public class CartDto
    {
        public int CartId { get; set; }
        public List<CartItemDto> CartItems { get; set; }
        public decimal TotalPrice() { return CartItems.Sum(x => x.Price * x.Quantity); }
    }
}
