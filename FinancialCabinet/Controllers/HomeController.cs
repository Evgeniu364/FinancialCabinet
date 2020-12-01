using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinancialCabinet.Models;
using FinancialCabinet.Service;

namespace FinancialCabinet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BankService bankService;

        public HomeController(ILogger<HomeController> logger, BankService bankService)
        {
            _logger = logger;
            this.bankService = bankService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.bankService.GetAllAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
