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
    public class IndividualController : ControllerBase
    {
        private readonly IIndividualManagementService _individualManagementService;
        private readonly UserManager<User> _userManager;

        public IndividualController(IIndividualManagementService individualManagementService, UserManager<User> userManager)
        {
            _individualManagementService = individualManagementService;
            _userManager = userManager;
        }
        
        [HttpPost]
        [Route("AddIndividual")]
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
        [Route("EditIndividual")]
        public async Task<IActionResult> EditIndividual(IndividualModel model)
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