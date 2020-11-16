using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Interface
{
    public interface ISingleCredit
    {
        public Guid ID { get; set; }
        public int MinSum { get; set; }
        public int MaxSum { get; set; }
        public string Currency { get; set; }
        public Guid PeriodID { get; set; }
        public Guid PercentID { get; set; }
        public Guid CreditID { get; set; }
        public bool IsGuarantorNeeded { get; set; }
        public bool IsIncomeCertificationNeeded { get; set; }
    }
}
