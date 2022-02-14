using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Entities.Dtos;
using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        public ShopController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = _productService.GetProductDetails((int)id);
            if (product == null)
            {
                return NotFound();
            }
            ProductDetails productDetails = new();
            productDetails.Product = product;
            productDetails.Categories = product.ProductCategories.Select(x => x.Category).ToList();
            return View(productDetails);
        }
        public IActionResult ProductList(string categoryName)
        {
            ProductList productList = new();
            productList.Products = _productService.GetProductsByCategory(categoryName);
            return View(productList);
        }        
    }
}
