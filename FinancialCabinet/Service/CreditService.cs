using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinancialCabinet.Data;
using FinancialCabinet.Entity;
using FinancialCabinet.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialCabinet.Service
{
    public class CreditService : ModelService<Credit, CreditModel, ApplicationDbContext, IMapper>
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public CreditService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public override async Task<List<CreditModel>> GetAllAsync()
        {
            List<Credit> entityList = await context.Credits.Include(e => e.SingleCreditList)
                .ThenInclude(e => e.Percent)
                .Include(e => e.SingleCreditList)
                .ThenInclude(e => e.Period).ToListAsync();
            List<CreditModel> modelList = mapper.Map<List<Credit>, List<CreditModel>>(entityList);
            return modelList;
        }

        public override async Task<List<CreditModel>> GetAllAsync(Dictionary<string, object> sortParams)
        {
            List<CreditModel> modelList = await GetAllAsync();
            int? sortType = (int?) sortParams["sortingType"];
            string currencyParam = sortParams["currencyParam"]?.ToString() ?? null;
            double? minAmount = (double?) sortParams["minAmount"];
            double? maxAmount = (double?) sortParams["maxAmount"];
            int? periodFrom = (int?) sortParams["periodFrom"];
            int? periodTo = (int?) sortParams["periodTo"];
            double? maxPercent = (double?) sortParams["maxPercent"];
            bool? isForBusiness = (bool?) sortParams["isForBusiness"];

            if (isForBusiness.HasValue)
            {
                modelList = modelList.Where(model => model.IsForBusiness == isForBusiness).ToList();
            }
            if (!string.IsNullOrEmpty(currencyParam))
            {
                modelList.ForEach(model => model.SingleCreditList = model.SingleCreditList.Where(singleCredit => singleCredit.Currency == currencyParam).ToList());
            }
            if (minAmount.HasValue)
            {
                modelList.ForEach(model => model.SingleCreditList = model.SingleCreditList.Where(singleCredit => singleCredit.MinSum >= minAmount.Value).ToList());
            }
            if (maxAmount.HasValue)
            {
                modelList.ForEach(model => model.SingleCreditList = model.SingleCreditList.Where(singleCredit => singleCredit.MaxSum >= maxAmount.Value).ToList());
            }
            if (periodFrom.HasValue && periodFrom.Value > 0)
            {
                modelList.ForEach(model => model.SingleCreditList = model.SingleCreditList.Where(singleCredit => singleCredit.Period.MinPeriod.HasValue && singleCredit.Period.MinPeriod >= periodFrom.Value).ToList());
            }
            if (periodTo.HasValue)
            {
                modelList.ForEach(model => model.SingleCreditList = model.SingleCreditList.Where(singleCredit => singleCredit.Period.MaxPeriod <= periodTo.Value).ToList());
            }
            if (maxPercent.HasValue)
            {
                modelList.ForEach(model => model.SingleCreditList = model.SingleCreditList.Where(singleCredit => singleCredit.Percent.MaxPercent <= maxPercent.Value).ToList());
            }
            modelList = modelList.Where(model => model.SingleCreditList.Count > 0).ToList();
            if (sortType.HasValue)
            {
                switch (sortType.Value)
                {
                    case 0:
                        modelList.ForEach(model => model.SingleCreditList = model.SingleCreditList.OrderByDescending(singleCredit => singleCredit.MinSum).Reverse().ToList());
                        return modelList.OrderByDescending(model => model.SingleCreditList.First().MinSum).Reverse().ToList();
                    case 1:
                        modelList.ForEach(model => model.SingleCreditList = model.SingleCreditList.OrderByDescending(singleCredit => singleCredit.MinSum).ToList());
                        return modelList.OrderByDescending(model => model.SingleCreditList.First().MinSum).ToList();
                    case 2:
                        modelList.ForEach(model => model.SingleCreditList = model.SingleCreditList.OrderBy(singleCredit => singleCredit.Period).ToList());
                        return modelList.OrderBy(model => model.SingleCreditList.First().Period).ToList();
                    case 3:
                        modelList.ForEach(model => model.SingleCreditList = model.SingleCreditList.OrderBy(singleCredit => singleCredit.Period).Reverse().ToList());
                        return modelList.OrderBy(model => model.SingleCreditList.First().Period).Reverse().ToList();
                    case 4:
                        modelList.ForEach(model => model.SingleCreditList = model.SingleCreditList.OrderBy(singleCredit => singleCredit.Percent.MaxPercent).ToList());
                        return modelList.Where(singleCredit => singleCredit.SingleCreditList.First().Percent.MaxPercent != 0).OrderBy(singleCredit => singleCredit.SingleCreditList.First().Percent.MaxPercent).Union(modelList.Where(model => model.SingleCreditList.All(singleCredit => singleCredit.Percent.MaxPercent == 0))).ToList();
                    case 5:
                        modelList.ForEach(model => model.SingleCreditList = model.SingleCreditList.OrderBy(singleCredit => singleCredit.Percent.MaxPercent).Reverse().ToList());
                        return modelList.Where(singleCredit => singleCredit.SingleCreditList.First().Percent.MaxPercent != 0).OrderBy(singleCredit => singleCredit.SingleCreditList.First().Percent.MaxPercent).Reverse().Union(modelList.Where(model => model.SingleCreditList.All(singleCredit => singleCredit.Percent.MaxPercent == 0))).ToList();
                }
            }
            return modelList;
        }
    }
}
