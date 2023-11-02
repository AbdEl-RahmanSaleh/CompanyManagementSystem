using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PresentationLayer.Helper;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser>userManager , SignInManager<ApplicationUser>signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region SignUp
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel registerVM)
        {
            if(ModelState.IsValid)
            {
                var User = new ApplicationUser
                {
                    Email = registerVM.Email,
                    UserName = registerVM.Email.Split('@')[0],
                    IsAgree = registerVM.IsAgree
                };

                var result = await _userManager.CreateAsync(User , registerVM.Password);
                await _userManager.AddToRoleAsync(User, "Normal User");

                if (result.Succeeded)  
                    return RedirectToAction("LogIn","Account");

                else
                    ModelState.AddModelError("", "Password must be at least 6 charachters Containing Numbers, UpperCase  and LowerCase Charachters and at least one NonAlphaNumeric charchter like' @,#,$'");

            }
            return View(registerVM);

        }
        #endregion

        #region LogIn
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel logInVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(logInVM.Email);
                if (user == null)
                     ModelState.AddModelError("", "Email Does't Exist ,Please Register First and try again");
                
                var isCorrectPassword = await _userManager.CheckPasswordAsync(user, logInVM.Password);
                if (isCorrectPassword)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, logInVM.Password,logInVM.RememberMe,false);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");

                }
                else if (user!=null && !isCorrectPassword)
                    ModelState.AddModelError("", "The password that you've entered is incorrect.");
            }
            return View(logInVM);
        }
        #endregion

        #region LogOut
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }
        #endregion

        #region ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgetPasswordVM.Email);
                if(user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var resetpasswordLink = Url.Action("ResetPassword", "Account", new { Email = forgetPasswordVM.Email, Token = token }, Request.Scheme);

                    var email = new Email
                    {
                        Title = "Reset Password",
                        Body = resetpasswordLink,
                        To = forgetPasswordVM.Email
                    };

                    EmailSettings.SendEmail(email);

                    return RedirectToAction("CompeleteFogetpassword");
                }
                ModelState.AddModelError("", "Email Does't Exist ");
            }
            return View(forgetPasswordVM);
        }
        public IActionResult CompeleteFogetpassword()
        {
            return View();
        } 
        #endregion

        #region ResetPassword
        public IActionResult ResetPassword(string email ,string token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewMode resetPasswordVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordVM.Email);
                
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, resetPasswordVM.Token, resetPasswordVM.Password);
                    if (result.Succeeded)
                        return RedirectToAction("LogIn", "Account");

                    else
                        ModelState.AddModelError("", "Password must be at least 6 charachters Containing Numbers, UpperCase  and LowerCase Charachters and at least one NonAlphaNumeric charchter like' @,#,$'");


                }
            }

            return View(resetPasswordVM);
        }
       
        #endregion

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
