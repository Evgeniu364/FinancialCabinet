using AutoMapper;
using FinancialCabinet.Data;
using FinancialCabinet.Entity;
using FinancialCabinet.Models;

namespace FinancialCabinet.Service
{
    public class CreditService : ModelService<Credit, CreditModel, ApplicationDbContext, IMapper>
    {
        public CreditService(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}
