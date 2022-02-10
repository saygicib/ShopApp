using ShopApp.Entities;
using System.Collections.Generic;

namespace ShopApp.DataAccess.Abstract
{
    public interface IProductDal : IRepositoryBaseDal<Product>
    {
        List<Product> GetPopularProductList();
        List<Product> GetProductsByCategory(string categoryName, int page, int pageSize);
        List<Product> GetAllWithPagging(int page, int pageSize);
        Product GetProductDetails(int id);
    }
}
