using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Entities.Dtos
{
    public class ProductDetails
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
