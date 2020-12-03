using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Entity
{
    public class Deposit: IDeposit
    {
        public Guid ID { get; set; }
        public Guid BankID { get; set; }
        public Bank Bank { get; set; }
        public string DepositName { get; set; }
        public bool IsForBusiness { get; set; }
        public List<SingleDeposit> SingleDepositList { get; set; }
    }
}
