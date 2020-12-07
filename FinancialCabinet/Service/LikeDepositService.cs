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
    public class LikeDepositService : ModelService<LikeDeposit, LikeDepositModel, ApplicationDbContext, IMapper>
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LikeDepositService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public override async Task<List<LikeDepositModel>> GetAllAsync()
        {
            return mapper.Map<List<LikeDepositModel>>(await context.LikeDeposit.Include(e => e.User).Include(e => e.SingleDeposit).ThenInclude(e => e.Deposit).ToListAsync());
        }
    }
}
