using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.Model
{
    public class CreditModel : ICredit
    {
        public Guid ID { get; set; }
        public int MinSum { get; set; }
        public int MaxSum { get; set; }
        public int Period { get; set; }
        public double Percent { get; set; }
        public int Age { get; set; }
        public bool IsGuarantor { get; set; }
        public bool IsIncomeCertification { get; set; }
        public Guid BankId { get; set; }
        public BankModel Bank { get; set; }
        public Guid GroupCreditId { get; set; }
        public List<UserModel> UserList { get; set; }
    }
}
