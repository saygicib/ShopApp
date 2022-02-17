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
    [AutoValidateAntiforgeryToken]
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
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callBackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token });//Kullanıcıya email olarak gidecek.

                //sendmail
                return RedirectToAction("login", "account");
            }

            ModelState.AddModelError("", "Bilinmeyen bir hata oluştu.");
            return View(dto);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginDto()
            {
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(dto);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Hesabınızı onaylayın.");
                return View(dto);
            }


            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, true/*Beni hatırla*/, false);
            if (result.Succeeded)
            {
                return Redirect(dto.ReturnUrl ?? "~/");
            }
            ModelState.AddModelError("", "Email adresi veya parola hatalı.");
            return View(dto);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData["message"] = "Geçersiz token";
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData["message"] = "Hesabınız onaylandı.";
                    return View();
                }
            }
            TempData["message"] = "Hesabınız onaylanmadı.";
            return View();
        }
    }
}
