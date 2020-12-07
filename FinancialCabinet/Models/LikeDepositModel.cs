using FinancialCabinet.Entity;
using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Models
{
    public class LikeDepositModel : ILikeDeposit
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid SingleDepositID { get; set; }
        public User User { get; set; }
        public SingleDeposit SingleDeposit { get; set; }
    }
}
