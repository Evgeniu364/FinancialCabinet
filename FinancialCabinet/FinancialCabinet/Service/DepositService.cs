using AutoMapper;
using FinancialCabinet.Database;
using FinancialCabinet.Entity;
using FinancialCabinet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Service
{
    public class DepositService : ModelService<Deposit, DepositModel, ApiDbContext, IMapper>
    {
        public DepositService(ApiDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}
