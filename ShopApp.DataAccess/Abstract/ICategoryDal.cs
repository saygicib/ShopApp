using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Abstract
{
    public interface ICategoryDal : IRepositoryBaseDal<Category>
    {
        Category GetByIdWithProducts(int id);
        void DeleteProductFromCategory(int productId, int categoryId);
    }
}
