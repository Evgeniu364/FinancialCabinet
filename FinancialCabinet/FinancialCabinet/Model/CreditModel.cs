using FinancialCabinet.Entity;
using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.Model
{
    public class CreditModel : ICredit
    {
        public Guid ID { get; set; }
        public Guid BankId { get; set; }
        public BankModel Bank { get; set; }
    }
}
