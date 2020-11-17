using AutoMapper;
using FinancialCabinet.Data;
using FinancialCabinet.Entity;
using FinancialCabinet.Interface;
using FinancialCabinet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Service
{
    public class BankService : ModelService<Bank, BankModel, ApplicationDbContext, IMapper>
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public BankService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
    }
}
