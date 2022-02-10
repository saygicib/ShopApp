using ShopApp.Entities;
using System.Collections.Generic;

namespace ShopApp.DataAccess.Abstract
{
    public interface IProductDal : IRepositoryBaseDal<Product>
    {
        IEnumerable<Product> GetPopularProductList();
        List<Product> GetProductsByCategory(string categoryName);
        Product GetProductDetails(int id);
    }
}
