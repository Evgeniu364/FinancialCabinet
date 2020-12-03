using AutoMapper;
using FinancialCabinet.Data;
using FinancialCabinet.Entity;
using FinancialCabinet.Models;
using FinancialCabinet.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Controllers
{
    public class CreditController : Controller
    {
        private readonly ParserService parserService;
        private readonly BankService bankService;
        private readonly CreditService creditService;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        public CreditController(ParserService parserService, BankService bankService, IMapper mapper, ApplicationDbContext context, CreditService creditService)
        {
            this.parserService = parserService;
            this.bankService = bankService;
            this.creditService = creditService;
            this.mapper = mapper;
            this.context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index(int? sortingType, string currencyParam, double? minAmount, double? maxAmount, int? periodFrom, int? periodTo, double? maxPercent)
        {
            List<CreditModel> creditModelList;
            //if (sortingType.HasValue)
            //{
            creditModelList = await creditService.GetAllAsync(new Dictionary<string, object>() { { "sortingType", sortingType },
                { "currencyParam", currencyParam },
                { "minAmount", minAmount },
                { "maxAmount", maxAmount },
                { "periodFrom", periodFrom },
                { "periodTo", periodTo },
                { "maxPercent", maxPercent },
                { "isForBusiness", User.IsInRole("Business")}
            });
            //}
            //else
            //    creditModelList = await creditService.GetAllAsync();
            return View(creditModelList);
        }

        public async Task<IActionResult> Test(int sortingParam)
        {
            return Content(sortingParam.ToString());
        }

        public async Task<IActionResult> StartParser()
        {
            List<BankModel> bankModelList = mapper.Map<List<Bank>, List<BankModel>>(parserService.ParseBanks());
            await bankService.DeleteAllAsync();
            await bankService.InsertManyAsync(bankModelList);
            return Content("complete");
        }


        [Authorize(Roles="Individual")]
        public async Task<IActionResult> TestInd()
        {
            return Content("Success individual!");
        }

        [Authorize(Roles="Business")]
        public async Task<IActionResult> TestBsn()
        {
            return Content("Success individual!");
        }
    }
}
