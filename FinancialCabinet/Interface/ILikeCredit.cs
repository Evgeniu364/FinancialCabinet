using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.Interface
{
    public interface ILikeCredit
    {
        public Guid UserID { get; set; }

        public Guid SingleCreditId { get; set; }
    }
}
