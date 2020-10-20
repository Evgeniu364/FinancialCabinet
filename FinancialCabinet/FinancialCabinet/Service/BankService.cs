using AutoMapper;
using FinancialCabinet.Database;
using FinancialCabinet.Entity;
using FinancialCabinet.Interface;
using FinancialCabinet.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Service
{
    public class BankService : ModelService<Bank, BankModel, ApiDbContext, IMapper>
    {
        private readonly ApiDbContext context;
        private readonly IMapper mapper;

        public BankService(ApiDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
    }
}
