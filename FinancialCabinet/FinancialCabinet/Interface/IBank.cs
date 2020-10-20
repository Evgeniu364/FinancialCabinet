using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.Interface
{
    public interface IBank
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string BankAccount { get; set; }
        public string BIK { get; set; }
        public string Information { get; set; }
    }
}
