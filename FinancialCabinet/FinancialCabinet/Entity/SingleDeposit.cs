using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialCabinet.Interface;

namespace FinancialCabinet.Entity
{
    public class SingleDeposit : ISingleDeposit
    {
        public Guid ID { get; set; }
        public int? Sum { get; set; }
        public string Currency { get; set; }
        public bool IsReplenishable { get; set; }
        public bool IsRevocable { get; set; }
        public Guid DepositID { get; set; }
        public Guid PeriodID { get; set; }
        public Guid PercentID { get; set; }
        public Deposit Deposit { get; set; }
        public Period Period { get; set; }
        public Percent Percent { get; set; }
        public List<LikeDeposit> LikeDepositList { get; set; }
    }
}
