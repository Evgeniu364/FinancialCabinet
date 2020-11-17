using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.Interface
{
    public interface ICredit
    {
        public Guid ID { get; set; }
        public Guid BankId { get; set; }
        public string CreditName { get; set; }
    }
}
