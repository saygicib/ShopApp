using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Abstract
{
    public interface IProductService
    {
        Product GetById(int id); 
        List<Product> GetAll();
        List<Product> GetPopularProducts();
        void Create(Product product);
        void Update(Product product);
        void Delete(Product product);
        Product GetProductDetails(int id);
    }
}
