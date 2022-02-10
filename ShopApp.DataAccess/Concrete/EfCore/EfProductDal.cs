using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfProductDal : EfCoreGenericRepositoryDal<Product, Context>, IProductDal
    {
        public List<Product> GetAllWithPagging(int page, int pageSize)
        {
            using (var context = new Context())
            {
                var products = context.Products.AsQueryable();
                products = products
                    .Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Category)
                    .Skip((page - 1) * pageSize).Take(pageSize);
                return products.ToList();
            }
        }

        public List<Product> GetPopularProductList()
        {
            throw new NotImplementedException();
        }

        public Product GetProductDetails(int id)
        {
            Product product = new();
            using (var context = new Context())
            {
                product = context.Products.Where(x => x.Id == id).Include(x => x.ProductCategories).ThenInclude(x => x.Category).FirstOrDefault();
            }
            return product;
        }

        public List<Product> GetProductsByCategory(string categoryName, int page, int pageSize)
        {
            using (var context = new Context())
            {
                var products = context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(categoryName))
                {
                    products = products
                        .Include(x => x.ProductCategories)
                        .ThenInclude(x => x.Category)
                        .Where(x => x.ProductCategories.Any(y => y.Category.Name.ToLower()==categoryName.ToLower())).Skip((page - 1) * pageSize).Take(pageSize);
                }
                return products.ToList();
            }
        }
    }
}
