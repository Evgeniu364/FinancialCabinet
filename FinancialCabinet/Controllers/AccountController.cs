using FinancialCabinet.Entity;
using FinancialCabinet.Service;
using FinancialCabinet.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            InitializeRoles();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {

            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                Individual individual = new Individual();
                LegalEntity legal = new LegalEntity();
                if (model.IsIndividual)
                {
                    individual = new Individual { Id = Guid.NewGuid(), Name = model.Name, LastName = model.LastName, Patronymic = model.Patronymic, DateOfBirth = Convert.ToDateTime(model.DateOfBirth), TypeDocument = model.TypeDocument, NumberDocument = model.DocumentNumber, Salary = Convert.ToDouble(model.Salary) };
                }
                else
                {
                    legal = new LegalEntity { Id = Guid.NewGuid(), CompanyName = model.CompanyName, Unp = Convert.ToInt32(model.Unp), NumberDocument = Convert.ToInt32(model.NumberDocument), CashTurnover = Convert.ToDouble(model.CashTurnover) };
                }

                var user = new User { 
                    UserName = model.Email,
                    Phone = model.Phone,
                    Email = model.Email,
                    Address = model.Address,
                    Individual = model.IsIndividual ? individual : null,
                    LegalEntity = model.IsIndividual ? null : legal,
                    IndividualID = model.IsIndividual ? individual.Id : Guid.Empty,
                    LegalEntityID = model.IsIndividual ? Guid.Empty : legal.Id,
                    EmailConfirmed = true };

                var result = await _userManager.CreateAsync(user, model.Password);

                //if (model.IsIndividual)
                //    await _userManager.AddToRoleAsync(user, "Individual");
                //else
                //    await _userManager.AddToRoleAsync(user, "Business");

                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                    return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
                }
                AddErrors(result);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion

        private void InitializeRoles()
        {
            if(!_roleManager.RoleExistsAsync("Individual").Result)
            {
                _roleManager.CreateAsync(new Role { Name = "Individual" });
            }

            if (!_roleManager.RoleExistsAsync("Business").Result)
            {
                _roleManager.CreateAsync(new Role { Name = "Business" });
            }
        }

    }
}
