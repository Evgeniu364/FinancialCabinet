using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Interface
{
    public interface IPercent
    {
        public Guid ID { get; set; }
        public double? MinPercent { get; set; }
        public double MaxPercent { get; set; }
        public bool IsInterval { get; set; }
        public Guid SingleDepositID { get; set; }
    }
}
