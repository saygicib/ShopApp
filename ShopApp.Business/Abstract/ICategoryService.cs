using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Abstract
{
    public interface ICategoryService
    {
        Category GetById(int id);
        Category GetByIdWithProducts(int id);
        List<Category> GetAll();
        void Create(Category category);
        void Update(Category category);
        void Delete(int id);
        void DeleteProductFromCategory(int productId,int categoryId);
    }
}
