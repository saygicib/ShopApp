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
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public void Create(Product product)
        {
            _productDal.Create(product);
        }

        public void Delete(int id)
        {
            var product = _productDal.GetById(id);
            if(product is not null)
                _productDal.Delete(product);           
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetAllWithPagging(int page, int pageSize)
        {
            return _productDal.GetAllWithPagging(page, pageSize);
        }

        public Product GetById(int id)
        {
            return _productDal.GetById(id);
        }

        public Product GetByIdWithCategories(int productId)
        {
            return _productDal.GetByIdWithCategories(productId);
        }

        public List<Product> GetListLastAddedNineProduct()
        {
            return _productDal.GetListLastAddedNineProduct();
        }

        public Product GetProductDetails(int id)
        {
            return _productDal.GetProductDetails(id);
        }

        public List<Product> GetProductsByCategory(string categoryName)
        {
            return _productDal.GetProductsByCategory(categoryName);
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }
    }
}
