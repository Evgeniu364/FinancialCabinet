using AutoMapper;
using FinancialCabinet.Data;
using FinancialCabinet.Models;
using FinancialCabinet.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.Controllers
{
    public class DepositeController: Controller
    {
        private readonly BankService bankService;
        private readonly DepositService depositService;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public DepositeController(BankService bankService, DepositService depositService, IMapper mapper, ApplicationDbContext context)
        {
            this.bankService = bankService;
            this.depositService = depositService;
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IActionResult> Index(int? sortingType, string currencyParam, int? periodFrom, int? periodTo, double? maxPercent)
        {
            List<DepositModel> depositModelsList;

            depositModelsList = await depositService.GetAllAsync(new Dictionary<string, object>() { { "sortingType", sortingType },
                { "currencyParam", currencyParam },
                { "periodFrom", periodFrom },
                { "periodTo", periodTo },
                { "maxPercent", maxPercent },
                { "isForBusiness", User.IsInRole("Business") }
            });

            return View(depositModelsList);
        }
    }
}
