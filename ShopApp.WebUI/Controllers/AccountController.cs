using Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Concrete;
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

                MailHelper.SendMail(dto.Email, "Confirm your email", $"<a href='http://localhost:6855{callBackUrl}'>Click</a> the link to confirm your email account.");

                TempData.Put("message", new ResultMessage() { Title = "Confirm your email", Message = "Click the link to confirm your email account.", Css = "warning" });

                return RedirectToAction("login", "account");
            }

            TempData.Put("message", new ResultMessage() { Title = "Exists", Message = "This email or username exists", Css = "warning" });
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
                TempData.Put("message", new ResultMessage() { Title = "Not Found", Message = "User Not Found", Css = "warning" });
                return View(dto);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                TempData.Put("message", new ResultMessage() { Title = "Confirm your account", Message = "Account confirmation required.", Css = "warning" });
                return View(dto);
            }


            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, true/*Beni hatırla*/, false);
            if (result.Succeeded)
            {
                return Redirect(dto.ReturnUrl ?? "~/");
            }
            TempData.Put("message", new ResultMessage() { Title = "Error", Message = "Email or password error.", Css = "danger" });

            return View(dto);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            TempData.Put("message", new ResultMessage() { Title = "Session Closed.", Message = "The session was securely closed.", Css = "warning" });

            return Redirect("~/");
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData.Put("message", new ResultMessage() { Title = "Account Confirmation.", Message = "Your information is incorrect for account confirmation.", Css = "danger" });
                return Redirect("~/");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData.Put("message", new ResultMessage() { Title = "Account Confirmation.", Message = "Your account has been successfully approved", Css = "success" });
                    return RedirectToAction("Login");
                }
            }
            TempData.Put("message", new ResultMessage() { Title = "Account Confirmation.", Message = "Your account could not be confirmed", Css = "danger" });
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData.Put("message", new ResultMessage() { Title = "Forgot Password", Message = "Your information is incorrect", Css = "danger" });
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                TempData.Put("message", new ResultMessage() { Title = "Forgot Password", Message = "Not user found for this email address.", Css = "danger" });
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callBackUrl = Url.Action("ResetPassword", "Account", new
            {
                token = token
            });//Kullanıcıya email olarak gidecek.


            //sendmail
            MailHelper.SendMail(email, "Reset Password", $"<a href='http://localhost:6855{callBackUrl}'>Click</a> the link for reset password.");

            TempData.Put("message", new ResultMessage() { Title = "Forgot Password", Message = "Sended mail for reset password", Css = "danger" });

            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            if (token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ResetPasswordDto { Token = token };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if(user==null)
            {
                return RedirectToAction("Index", "Home");
            }

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(dto);
        }
        public async Task<IActionResult> Accessdenied()
        {
            return View();
        }
    }
}
