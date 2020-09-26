using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Model
{
    public class DepositModel: IDeposit
    {
        public Guid ID { get; set; }
        public int MinSum { get; set; }
        public int MaxSum { get; set; }
        public int Period { get; set; }
        public double Percent { get; set; }
        public string Currency { get; set; }
        public bool IsReplenishable { get; set; }
        public bool IsRevocable { get; set; }
        public List<UserModel> UserList { get;set; }
    }
}
