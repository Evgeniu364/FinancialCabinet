using FinancialCabinet.Entity;
using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.Model
{
    public class BankModel : IBank
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<string> Phones { get; set; }
        public string BankAccount { get; set; }
        public string BIK { get; set; }
        public string Information { get; set; }
        public List<Deposit> DepositList { get; set; }
        public List<Credit> CreditList { get; set; }
    }
}
