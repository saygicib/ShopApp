using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.Memory
{
    public class MemoryProductDal : IProductDal
    {
        public void Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> predicate = null)
        {
            var products = new List<Product>()
            {
                new Product() {Id=1,Name="Samsung",ImageUrl="1.jpg",Price=1000},
                new Product() {Id=2,Name="IPhone",ImageUrl="2.jpg",Price=2000},
                new Product() {Id=3,Name="Xiaomi",ImageUrl="3.jpg",Price=3000}
            };
            return products;
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetOne(Expression<Func<Product, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetPopularProductList()
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
