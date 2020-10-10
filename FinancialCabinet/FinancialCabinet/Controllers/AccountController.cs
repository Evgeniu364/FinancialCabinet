using System;
using System.Threading.Tasks;
using FinancialCabinet.Database;
using FinancialCabinet.Entity;
using FinancialCabinet.Interface;
using FinancialCabinet.Model;
using FinancialCabinet.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancialCabinet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private ApiDbContext _db;
        private readonly IIndividualManagementService _individualManagementService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApiDbContext context,
            IIndividualManagementService individualManagementService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = context;
            _individualManagementService = individualManagementService;
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return Content("Get Register");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email, UserName = model.Email, DateRegistration = DateTime.Now,
                    Phone = model.Phone, Address = model.Address
                }; // потом передлеать, когда будет мапинг

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return this.Ok(model);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return Content("Post Register GG");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return Content("Successful auth");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }

            return Content("Successful auth");
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Content("Successful out");
        }

        [HttpPost]
        [Route("Edit/AddIndividual")]
        public async Task<IActionResult> AddIndividual(IndividualModel model)
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                Individual ind = _individualManagementService.CreateIndividual(model, user).Result;
                user.IndividualID = ind.Id;
                user.Individual = ind;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Content("Ok");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return Content("GG");
        }
        
        [HttpPost]
        [Route("Edit/EditIndividual")]
        public async Task<IActionResult> EditIndividual(EditIndividualModel model)
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user.IndividualID != null &&
                _individualManagementService.Get((Guid) user.IndividualID, out var individual))
            {
                if (_individualManagementService.EditIndividual(individual.Id, model).Result)
                {
                    return Content("Successfully");
                }
                else
                {
                    return Content("GG");
                }
            }
            return Content("GG");
        }
    }
}