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
    public class LikeCreditService : ModelService<LikeCredit, LikeCreditModel, ApplicationDbContext, IMapper>
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LikeCreditService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public override async Task<List<LikeCreditModel>> GetAllAsync()
        {
            return mapper.Map<List<LikeCreditModel>>(await context.LikeCredit.Include(e => e.User).Include(e => e.SingleCredit).ThenInclude(e => e.Credit).ToListAsync());
        }
    }
}
