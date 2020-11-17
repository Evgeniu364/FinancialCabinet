using System.Collections.Generic;
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
    }
}
