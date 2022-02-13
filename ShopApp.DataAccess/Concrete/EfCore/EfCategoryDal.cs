using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCategoryDal : EfCoreGenericRepositoryDal<Category, Context>, ICategoryDal
    {
        public Category GetByIdWithProducts(int id)
        {
            using (var context = new Context())
            {
                var category = context.Categories.Where(x => x.Id == id).Include(x => x.ProductCategories).ThenInclude(x => x.Product).FirstOrDefault();
                return category;
            }
        }
    }
}
