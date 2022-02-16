using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Entities.Dtos;
using ShopApp.WebUI.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View(new RegisterDto());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                //token
                //sendmail
                return RedirectToAction("login","account");
            }

            ModelState.AddModelError("", "Bilinmeyen bir hata oluştu.");
            return View(dto);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginDto());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto,string returnUrl=null)
        {
            returnUrl = returnUrl ?? "~/";
            if(!ModelState.IsValid)
            {
                return View(dto);
            }
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user==null)
            {
                ModelState.AddModelError("","Kullanıcı bulunamadı.");
                return View(dto);
            }
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, true/*Beni hatırla*/, false);
            if(result.Succeeded)
            {
                return Redirect(returnUrl);
            }
            return View();
        }
    }
}
