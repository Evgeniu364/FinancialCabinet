using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.Entity
{
    public class Credit : ICredit
    {
        public Guid ID { get; set; }
        public Guid BankId { get; set; }
        public Bank Bank { get; set; }
        public bool IsForBusiness { get; set; }
        public string CreditName { get; set; }
        public string CreditDescription { get; set; }
        public List<SingleCredit> SingleCreditList { get; set; }
    }
}
