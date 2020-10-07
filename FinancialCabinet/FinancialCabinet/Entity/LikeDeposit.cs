using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Entity
{
    public class LikeDeposit: ILikeDeposit
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid DepositID { get; set; }
        public User User { get; set; }
        public Deposit Deposit { get; set; }
    }
}
