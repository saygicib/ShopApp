using IyzipayCore;
using IyzipayCore.Model;
using IyzipayCore.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Entities.Concrete;
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
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ICartService cartService, UserManager<ApplicationUser> userManager, IOrderService orderService)
        {
            _cartService = cartService;
            _userManager = userManager;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));
            if (cart != null)
            {
                return View(new CartDto()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(x => new CartItemDto()
                    {
                        CartItemId = x.Id,
                        ProductId = x.Product.Id,
                        Name = x.Product.Name,
                        Price = x.Product.Price,
                        ImageUrl = x.Product.ImageUrl,
                        Quantity = x.Quantity
                    }).ToList()
                });
            }
            return Redirect("/");
        }
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            _cartService.AddToCart(_userManager.GetUserId(User), productId, quantity);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            _cartService.DeleteFromCart(_userManager.GetUserId(User), productId);
            return RedirectToAction("Index");
        }
        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            var orderDto = new OrderDto();

            orderDto.CartDto = new CartDto()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(x => new CartItemDto()
                {
                    CartItemId = x.Id,
                    ProductId = x.Product.Id,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    ImageUrl = x.Product.ImageUrl,
                    Quantity = x.Quantity
                }).ToList()
            };

            return View(orderDto);
        }
        [HttpPost]
        public IActionResult Checkout(OrderDto dto)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var cart = _cartService.GetCartByUserId(userId);

                dto.CartDto = new CartDto()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(x => new CartItemDto()
                    {
                        CartItemId = x.Id,
                        ProductId = x.Product.Id,
                        Name = x.Product.Name,
                        Price = x.Product.Price,
                        ImageUrl = x.Product.ImageUrl,
                        Quantity = x.Quantity
                    }).ToList()
                };

                var payment = PaymentProcess(dto);
                if (payment.Status == "success")
                {
                    SaveOrder(dto, payment, userId);
                    ClearCart(cart.Id);
                    return View("Success");
                }
            }
            //sipariş
            return View(dto);
        }

        private void SaveOrder(OrderDto dto, Payment payment, string userId)
        {
            var order = new Order();
            order.OrderNumber = new Random().Next(111111, 999999).ToString();
            order.Status = EnumOrderStatus.Completed;
            order.PaymentTypes = EnumPaymentType.CrediCard;
            order.PaymentId = payment.PaymentId;
            order.ConversationId = payment.ConversationId;
            order.CreatedDate = DateTime.Now;
            order.FirstName = dto.FirstName;
            order.LastName = dto.LastName;
            order.Email = dto.Email;
            order.Phone = dto.Phone;
            order.Address = dto.Address;
            order.UserId = userId;
            foreach (var item in dto.CartDto.CartItems)
            {
                var orderItem = new OrderItem()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                };
                order.OrderItems.Add(orderItem);
            }
            _orderService.Create(order);
        }

        private void ClearCart(int cartId)
        {
            _cartService.ClearCart(cartId);
        }

        private Payment PaymentProcess(OrderDto dto)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-PPC2kJ4ShRI23RcZ6pPXeCLwIjxXzx9X";
            options.SecretKey = "sandbox-SnG6JVkT9a4JT32rymkbeJcsv1atCrKR";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = dto.CartDto.TotalPrice().ToString().Split(',')[0];
            request.PaidPrice = dto.CartDto.TotalPrice().ToString().Split(',')[0];
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = dto.CartDto.CartId.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = dto.CardName;
            paymentCard.CardNumber = dto.CardNumber;
            paymentCard.ExpireMonth = dto.ExpirationMonth;
            paymentCard.ExpireYear = dto.ExpirationYear;
            paymentCard.Cvc = dto.CVV;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            //PaymentCard paymentCard = new PaymentCard();
            //paymentCard.CardHolderName = "John Doe";
            //paymentCard.CardNumber = "5528790000000008";
            //paymentCard.ExpireMonth = "12";
            //paymentCard.ExpireYear = "2030";
            //paymentCard.Cvc = "123";
            //paymentCard.RegisterCard = 0;
            //request.PaymentCard = paymentCard;



            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basketItem;
            foreach (var item in dto.CartDto.CartItems)
            {
                basketItem = new BasketItem();
                basketItem.Id = item.ProductId.ToString();
                basketItem.Name = item.Name;
                basketItem.Category1 = "phone";
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                basketItem.Price = (item.Price * item.Quantity).ToString().Split(',')[0];

                basketItems.Add(basketItem);
            }


            request.BasketItems = basketItems;

            return Payment.Create(request, options);
        }
        public IActionResult GetOrders()
        {
            var orders = _orderService.GetOrders(_userManager.GetUserId(User));
            var orderListDto = new List<OrderListDto>();
            OrderListDto orderDto;
            foreach (var item in orders)
            {
                orderDto = new OrderListDto();
                orderDto.OrderId = item.Id;
                orderDto.OrderNumber = item.OrderNumber;
                orderDto.CreatedDate = item.CreatedDate;
                orderDto.Phone = item.Phone;
                orderDto.FirstName = item.FirstName;
                orderDto.LastName = item.LastName;
                orderDto.Email = item.Email;
                orderDto.Address = item.Address;
                orderDto.City = item.City;
                orderDto.Status = item.Status;

                orderDto.OrderItems = item.OrderItems.Select(x => new OrderItemDto()
                {
                    OrderItemId = x.Id,
                    Name = x.Product.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    ImageUrl = x.Product.ImageUrl,
                }).ToList();
                orderListDto.Add(orderDto);
            }
            return View(orderListDto);
        }
    }
}
