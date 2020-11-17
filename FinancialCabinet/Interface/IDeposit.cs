using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Interface
{
    public interface IDeposit
    {
        public Guid ID { get; set; }
        public Guid BankID { get; set; }
        public string DepositName { get; set; }
    }
}
