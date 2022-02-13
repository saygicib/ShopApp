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
            var product = _productService.GetById((int)productId);
            UpdateProduct updateProduct = new UpdateProduct();
            updateProduct.Id = product.Id;
            updateProduct.Name = product.Name;
            updateProduct.Description = product.Description;
            updateProduct.ImageUrl = product.ImageUrl;
            updateProduct.Price = product.Price;
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
        public IActionResult CategoryList()
        {
            return View(new CategoryList()
            {
                Categories = _categoryService.GetAll()
            });
        }
    }
}
