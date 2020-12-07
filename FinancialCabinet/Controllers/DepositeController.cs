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
        private readonly LikeDepositService likeDepositService;

        public DepositeController(BankService bankService, DepositService depositService, IMapper mapper, ApplicationDbContext context, RecomendationSystem recomendation, UserManager<User> userManager, LikeDepositService likeDepositService)
        {
            this.bankService = bankService;
            this.depositService = depositService;
            this.mapper = mapper;
            this.context = context;
            this._recomendation = recomendation;
            this._userManager = userManager;
            this.likeDepositService = likeDepositService;
        }

        public async Task<IActionResult> Index(int? sortingType, string currencyParam, int? periodFrom, int? periodTo, double? maxPercent, bool? isRecomendation, bool? isLikeDeposits)
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
                { "isForBusiness", User.IsInRole("Business") },
                { "isLikeDeposits", isLikeDeposits},
                { "userId", _userManager.GetUserAsync(HttpContext.User).Result.Id}
            });

            return View(depositModelsList);
        }

        [HttpGet]
        public async Task<IActionResult> Like(Guid depositId)
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);
            LikeDepositModel likeDepositModel = likeDepositService.GetAllAsync().Result.Where(e => e.UserID == _userManager.GetUserAsync(HttpContext.User).Result.Id && e.SingleDepositID == depositId).FirstOrDefault();
            if (likeDepositModel == null)
            {
                LikeDepositModel likeDeposit = new LikeDepositModel { User = user, SingleDepositID = depositId };
                await likeDepositService.InsertAsync(likeDeposit);
                return RedirectToAction("Index");
            }
            else
            {
                await likeDepositService.DeleteByIdAsync(likeDepositModel.ID);
            }
            return RedirectToAction("Index");
        }
    }
}
