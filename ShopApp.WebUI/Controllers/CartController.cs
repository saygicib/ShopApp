using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Entities.Dtos;
using ShopApp.WebUI.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));
            return View(new CartDto()
            {
                CartId=cart.Id,
                CartItems=cart.CartItems.Select(x=> new CartItemDto() 
                {
                    CartItemId= x.Id,
                    ProductId=x.Product.Id,
                    Name=x.Product.Name,
                    Price=x.Product.Price,
                    ImageUrl=x.Product.ImageUrl,
                    Quantity=x.Quantity
                }).ToList()
            });
        }
        [HttpPost]
        public IActionResult AddToCart()
        {
            return View();
        }
    }
}
