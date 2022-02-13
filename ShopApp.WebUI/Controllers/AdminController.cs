using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Entities;
using ShopApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult ProductList()
        {
            ProductList productList = new();
            productList.Products = _productService.GetAll();
            return View(productList);
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(AddProduct product)
        {
            Product addedProduct = new Product();
            addedProduct.Name = product.Name;
            addedProduct.ImageUrl = product.ImageUrl;
            addedProduct.Description = product.Description;
            addedProduct.Price = product.Price;
            _productService.Create(addedProduct);
            return Redirect("ProductList");
        }
        [HttpGet]
        public IActionResult UpdateProduct(int? productId)
        {
            if (productId==null)
            {
                return NotFound();
            }
            Product product = _productService.GetByIdWithCategories((int)productId);
            UpdateProduct updateProduct = new UpdateProduct();
            updateProduct.Id = product.Id;
            updateProduct.Name = product.Name;
            updateProduct.Description = product.Description;
            updateProduct.ImageUrl = product.ImageUrl;
            updateProduct.Price = product.Price;
            updateProduct.SelectedCategories = product.ProductCategories.Select(x => x.Category).ToList();
            ViewBag.Categories = _categoryService.GetAll();
            return View(updateProduct);
        }
        [HttpPost]
        public IActionResult UpdateProduct(UpdateProduct updateProduct)
        {
            Product product = new Product();
            product.Id = updateProduct.Id;
            product.Name = updateProduct.Name;
            product.ImageUrl = updateProduct.ImageUrl;
            product.Description = updateProduct.Description;
            product.Price = updateProduct.Price;
            _productService.Update(product);
            return Redirect("ProductList");
        }
        public IActionResult DeleteProduct(int? productId)
        {
            _productService.Delete((int)productId);
            return Redirect("ProductList");
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(AddCategory category)
        {
            Category addedCategory = new Category();
            addedCategory.Name = category.Name;
            _categoryService.Create(addedCategory);
            return Redirect("CategoryList");
        }
        public IActionResult CategoryList()
        {
            return View(new CategoryList()
            {
                Categories = _categoryService.GetAll()
            });
        }
        [HttpGet]
        public IActionResult UpdateCategory(int? categoryId)
        {
            if (categoryId == null)
            {
                return NotFound();
            }
            var category = _categoryService.GetByIdWithProducts((int)categoryId);
            UpdateCategory updateCategory = new UpdateCategory();
            updateCategory.Id = category.Id;
            updateCategory.Name = category.Name;
            updateCategory.Products = category.ProductCategories.Select(x => x.Product).ToList();
            return View(updateCategory);
        }
        [HttpPost]
        public IActionResult UpdateCategory(UpdateCategory updateCategory)
        {
            Category category = new Category();
            category.Id = updateCategory.Id;
            category.Name = updateCategory.Name;
            _categoryService.Update(category);
            return Redirect("CategoryList");
        }
        public IActionResult DeleteCategory(int? categoryId )
        {
            _categoryService.Delete((int)categoryId);
            return Redirect("CategoryList");
        }
        public IActionResult DeleteProductFromCategory(int? productId,int? categoryId)
        {
            _categoryService.DeleteProductFromCategory((int)productId,(int)categoryId);
            return Redirect("CategoryList");
        }
    }
}
