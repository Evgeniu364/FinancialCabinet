using AutoMapper;
using FinancialCabinet.Data;
using FinancialCabinet.Models;
using FinancialCabinet.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialCabinet.Entity;
using Microsoft.AspNetCore.Identity;

namespace FinancialCabinet.Controllers
{
    public class DepositeController: Controller
    {
        private readonly BankService bankService;
        private readonly DepositService depositService;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly RecomendationSystem _recomendation;
        private readonly UserManager<User> _userManager;

        public DepositeController(BankService bankService, DepositService depositService, IMapper mapper, ApplicationDbContext context, RecomendationSystem recomendation, UserManager<User> userManager)
        {
            this.bankService = bankService;
            this.depositService = depositService;
            this.mapper = mapper;
            this.context = context;
            this._recomendation = recomendation;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index(int? sortingType, string currencyParam, int? periodFrom, int? periodTo, double? maxPercent, bool? isRecomendation)
        {
            List<DepositModel> depositModelsList;
            
            if (isRecomendation.HasValue && isRecomendation.Value)
            {
                return View( await _recomendation.GetRecomendationDeposits(_userManager.GetUserAsync(HttpContext.User).Result.Id));
            }

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
