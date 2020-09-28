using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.Entity
{
    public class GroupUserCredit : IGroupUserCredit
    {
        public Guid ID { get; set; }
        public Guid GroupCreditId { get; set; }
    }
}
