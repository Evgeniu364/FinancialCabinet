using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Entity
{
    public class GroupDeposit
    {
        public Guid ID { get; set; }
        public int GroupNumber { get; set; }
        public Guid SingleDepositID { get; set; }
        public SingleDeposit SingleDeposit { get; set; }
        public double k { get; set; }
    }
}
