using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FinancialCabinet.Interface
{
    public interface IGroupUserCredit
    {
        public Guid ID { get; set; }

        public Guid GroupCreditId { get; set; }
    }
}
