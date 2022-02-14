using Core.Aspect.Autofac.Validation;
using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities.Concrete;
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


        [ValidationAspect(typeof(Product))]
        public bool Create(Product product)
        {
            if (Validate(product))
            {
                _productDal.Create(product);
                return true;
            }
            return false;
        }

        public void Delete(int id)
        {
            var product = _productDal.GetById(id);
            if (product is not null)
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

        public void UpdateWithCategories(Product product, int[] categoryIds)
        {
            _productDal.UpdateWithCategories(product, categoryIds);
        }
        public string ErrorMessage { get; set; }
        public bool Validate(Product entity)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(entity.Name))
            {
                ErrorMessage += "Ürün ismi girmelisiniz.\n";
                isValid = false;
            }
            if (string.IsNullOrEmpty(entity.Description))
            {
                ErrorMessage += "Açıklama on karakterden uzun olmalıdır.\n";
                isValid = false;
            }
            return isValid;
        }
    }
}
