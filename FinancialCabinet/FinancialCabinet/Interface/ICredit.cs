using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.Interface
{
    public interface ICredit
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

        public Guid GroupCreditId { get; set; }

    }
}
