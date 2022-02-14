using ShopApp.Entities.Concrete;
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
        List<Product> GetListLastAddedNineProduct();
        List<Product> GetProductsByCategory(string categoryName);
        List<Product> GetAllWithPagging(int page,int pageSize);
        void Create(Product product);
        void Update(Product product);
        void UpdateWithCategories(Product product, int[] categoryIds);
        void Delete(int id);
        Product GetProductDetails(int id);
        Product GetByIdWithCategories(int productId);
    }
}
