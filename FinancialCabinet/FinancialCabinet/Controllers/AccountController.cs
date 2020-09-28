using System.Threading.Tasks;
using FinancialCabinet.Entity;
using FinancialCabinet.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinancialCabinet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
 
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return Content("Get Register");
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
        {
            if(ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email}; // потом передлеать, когда будет мапинг
                
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
    }
}