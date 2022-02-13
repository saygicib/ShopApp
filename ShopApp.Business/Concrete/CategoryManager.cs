using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Create(Category category)
        {
            _categoryDal.Create(category);
        }

        public void Delete(int id)
        {
            var category = _categoryDal.GetById(id);
            _categoryDal.Delete(category);
        }

        public void DeleteProductFromCategory(int productId,int categoryId)
        {
            _categoryDal.DeleteProductFromCategory(productId, categoryId);
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetAll();
        }

        public Category GetById(int id)
        {
            return _categoryDal.GetById(id);
        }

        public Category GetByIdWithProducts(int id)
        {
            return _categoryDal.GetByIdWithProducts(id);
        }

        public void Update(Category category)
        {
            _categoryDal.Update(category);
        }
    }
}
