using AutoMapper;
using FinancialCabinet.Data;

using FinancialCabinet.Entity;
using FinancialCabinet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Service
{
    public class RecomendationSystem
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly CreditService _creditService;
        private readonly DepositService _depositService;
        public RecomendationSystem(ApplicationDbContext dbContext, CreditService creditService, DepositService depositService)
        {
            _dbContext = dbContext;
            _creditService = creditService;
            _depositService = depositService;
        }


        public async Task<List<CreditModel>> GetRecomendationCredits(Guid id)
        {
            List<CreditModel> creditlist = await _creditService.GetAllAsync();
            List<int> index = new List<int>();
            Random rnd = new Random(Seed:id.ToByteArray()[0]);
            
            while (index.Count < 5)
            {
                int newIndex = rnd.Next(0, creditlist.Count);
                if (!index.Contains(newIndex))
                {
                    index.Add(newIndex);
                }
            }
            return index.Select(i => creditlist[i]).ToList();
        }
        
        public async Task<List<DepositModel>> GetRecomendationDeposits(Guid id)
        {
            List<DepositModel> depositlist = await _depositService.GetAllAsync();
            List<int> index = new List<int>();
            Random rnd = new Random(Seed:id.ToByteArray()[0]);
            while (index.Count < 5)
            {
                int newIndex = rnd.Next(0, depositlist.Count);
                if (!index.Contains(newIndex))
                {
                    index.Add(newIndex);
                }
            }
            return index.Select(i => depositlist[i]).ToList();
        }
    }
}