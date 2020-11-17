using AutoMapper;
using FinancialCabinet.Data;
using FinancialCabinet.Entity;
using FinancialCabinet.Models;
using FinancialCabinet.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Controllers
{
    public class CreditController: Controller
    {
        private readonly ParserService parserService;
        private readonly BankService bankService;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        public CreditController(ParserService parserService, BankService bankService, IMapper mapper, ApplicationDbContext context)
        {
            this.parserService = parserService;
            this.bankService = bankService;
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<BankModel> bankModelList = await bankService.GetAllAsync();
            return View(bankModelList);
        }

        public async Task<IActionResult> StartParser()
        {
            List<BankModel> bankModelList = mapper.Map<List<Bank>, List<BankModel>>(parserService.ParseBanks());
            await bankService.DeleteAllAsync();
            await bankService.InsertManyAsync(bankModelList);
            return Content("complete");
        }
    }
}
