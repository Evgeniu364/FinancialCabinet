using AutoMapper;
using FinancialCabinet.Data;

using FinancialCabinet.Entity;
using FinancialCabinet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Service
{
    public class DepositService : ModelService<Deposit, DepositModel, ApplicationDbContext, IMapper>
    {
        public DepositService(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}
