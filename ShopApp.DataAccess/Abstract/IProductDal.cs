using ShopApp.Entities;
using System.Collections.Generic;

namespace ShopApp.DataAccess.Abstract
{
    public interface IProductDal : IRepositoryBaseDal<Product>
    {
        IEnumerable<Product> GetPopularProductList();
    }
}
