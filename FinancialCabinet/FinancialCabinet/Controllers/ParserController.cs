using AutoMapper;
using FinancialCabinet.Entity;
using FinancialCabinet.Model;
using FinancialCabinet.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParserController: ControllerBase
    {

        private readonly ParserService parser;
        private readonly BankService bankService;
        private readonly IMapper mapper;
        
        public ParserController(ParserService parser, BankService bankService, IMapper mapper)
        {
            this.parser = parser;
            this.bankService = bankService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> TestParser()
        {
            List<Bank> entity = parser.ParseBanks();
            List<BankModel> model = mapper.Map<List<BankModel>>(entity);
            await bankService.DeleteAllAsync();
            await bankService.InsertManyAsync(model);
            return Content("success");
        }
    }
}
