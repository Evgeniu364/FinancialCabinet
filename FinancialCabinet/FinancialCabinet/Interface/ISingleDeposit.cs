using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Interface
{
    public interface ISingleDeposit
    {
        public Guid ID { get; set; }
        public int? Sum { get; set; }
        public string Currency { get; set; }
        public bool IsReplenishable { get; set; }
        public bool IsRevocable { get; set; }
        public Guid DepositID { get; set; }
        public Guid PeriodID { get; set; }
        public Guid PercentID { get; set; }
    }
}
