using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Entity
{
    public class GroupCredit
    {
        public Guid ID { get; set; }
        public int GroupNumber { get; set; }
        public Guid SingleCreditID { get; set; }
        public SingleCredit SingleCredit { get; set; }
        public double k { get; set; }
    }
}
