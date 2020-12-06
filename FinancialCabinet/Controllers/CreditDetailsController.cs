using FinancialCabinet.Models;
using FinancialCabinet.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Controllers
{
    public class CreditDetailsController : Controller
    {

        private readonly CreditService creditService;

        public CreditDetailsController(CreditService creditService)
        {
            this.creditService = creditService;
        }

        public async Task<IActionResult> Index(Guid creditId)
        {
            CreditModel credit = await creditService.GetAsync(creditId);
            if (credit == null || string.IsNullOrEmpty(credit.CreditDescription))
                return NotFound();
            return View(credit);
        }
    }
}
