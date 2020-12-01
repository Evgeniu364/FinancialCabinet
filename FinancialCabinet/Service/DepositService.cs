using AutoMapper;
using FinancialCabinet.Data;

using FinancialCabinet.Entity;
using FinancialCabinet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Service
{
    public class DepositService : ModelService<Deposit, DepositModel, ApplicationDbContext, IMapper>
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DepositService(ApplicationDbContext context, IMapper mapper) : base(context, mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }

        public override async Task<List<DepositModel>> GetAllAsync()
        {
            List<Deposit> entityList = await context.Deposits.Include(e => e.SingleDepositList)
                .ThenInclude(e => e.Percent)
                .Include(e => e.SingleDepositList)
                .ThenInclude(e => e.Period).ToListAsync();
            List<DepositModel> modelList = mapper.Map<List<Deposit>, List<DepositModel>>(entityList);
            return modelList;
        }

        public override async Task<List<DepositModel>> GetAllAsync(Dictionary<string, object> sortParams)
        {
            List<DepositModel> modelList = await this.GetAllAsync();
            int? sortType = (int?)sortParams["sortingType"];
            string currencyParam = sortParams["currencyParam"]?.ToString() ?? null;
            int? periodFrom = (int?)sortParams["periodFrom"];
            int? periodTo = (int?)sortParams["periodTo"];
            double? maxPercent = (double?)sortParams["maxPercent"];
            if (!string.IsNullOrEmpty(currencyParam))
            {
                modelList.ForEach(model => model.SingleDepositList = model.SingleDepositList.Where(singleCredit => singleCredit.Currency == currencyParam).ToList());
            }
            if (periodFrom.HasValue && periodFrom.Value > 0)
            {
                modelList.ForEach(model => model.SingleDepositList = model.SingleDepositList.Where(singleCredit => singleCredit.Period.MinPeriod.HasValue && singleCredit.Period.MinPeriod >= periodFrom.Value).ToList());
            }
            if (periodTo.HasValue)
            {
                modelList.ForEach(model => model.SingleDepositList = model.SingleDepositList.Where(singleCredit => singleCredit.Period.MaxPeriod <= periodTo.Value).ToList());
            }
            if (maxPercent.HasValue)
            {
                modelList.ForEach(model => model.SingleDepositList = model.SingleDepositList.Where(singleCredit => singleCredit.Percent.MaxPercent <= maxPercent.Value).ToList());
            }
            modelList = modelList.Where(model => model.SingleDepositList.Count > 0).ToList();
            if (sortType.HasValue)
            {
                switch (sortType.Value)
                {
                    case 0:
                        modelList.ForEach(model => model.SingleDepositList = model.SingleDepositList.OrderBy(singleCredit => singleCredit.Period).ToList());
                        return modelList.OrderBy(model => model.SingleDepositList.First().Period).ToList();
                    case 1:
                        modelList.ForEach(model => model.SingleDepositList = model.SingleDepositList.OrderBy(singleCredit => singleCredit.Period).Reverse().ToList());
                        return modelList.OrderBy(model => model.SingleDepositList.First().Period).Reverse().ToList();
                    case 2:
                        modelList.ForEach(model => model.SingleDepositList = model.SingleDepositList.OrderBy(singleCredit => singleCredit.Percent.MaxPercent).ToList());
                        return modelList.Where(singleCredit => singleCredit.SingleDepositList.First().Percent.MaxPercent != 0).OrderBy(singleCredit => singleCredit.SingleDepositList.First().Percent.MaxPercent).Union(modelList.Where(model => model.SingleDepositList.All(singleCredit => singleCredit.Percent.MaxPercent == 0))).ToList();
                    case 3:
                        modelList.ForEach(model => model.SingleDepositList = model.SingleDepositList.OrderBy(singleCredit => singleCredit.Percent.MaxPercent).Reverse().ToList());
                        return modelList.Where(singleCredit => singleCredit.SingleDepositList.First().Percent.MaxPercent != 0).OrderBy(singleCredit => singleCredit.SingleDepositList.First().Percent.MaxPercent).Reverse().Union(modelList.Where(model => model.SingleDepositList.All(singleCredit => singleCredit.Percent.MaxPercent == 0))).ToList();
                }
            }
            return modelList;
        }
    }
}
