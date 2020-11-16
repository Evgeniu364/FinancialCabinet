using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Interface
{
    public interface ILikeDeposit
    {
        public Guid UserID { get; set; }
        public Guid SingleDepositID { get; set; }
    }
}
