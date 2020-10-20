using AutoMapper;
using FinancialCabinet.Database;
using FinancialCabinet.Entity;
using FinancialCabinet.Model;

namespace FinancialCabinet.Service
{
    public class CreditService : ModelService<Credit, CreditModel, ApiDbContext, IMapper>
    {
        public CreditService(ApiDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}
