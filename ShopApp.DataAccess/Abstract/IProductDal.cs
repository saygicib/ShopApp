using ShopApp.Entities.Concrete;
using System.Collections.Generic;

namespace ShopApp.DataAccess.Abstract
{
    public interface IProductDal : IRepositoryBaseDal<Product>
    {
        List<Product> GetListLastAddedNineProduct();
        List<Product> GetProductsByCategory(string categoryName);
        List<Product> GetAllWithPagging(int page, int pageSize);
        Product GetProductDetails(int id);
        Product GetByIdWithCategories(int productId);
        void UpdateWithCategories(Product product, int[] categoryIds);
    }
}
