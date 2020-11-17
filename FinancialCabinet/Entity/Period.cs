using FinancialCabinet.Infrastructure.Enum;
using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Entity
{
    public class Period: IPeriod
    {
        public Guid ID { get; set; }
        public int? MinPeriod { get; set; }
        public int MaxPeriod { get; set; }
        public PeriodTypeEnum? MinPeriodType { get; set; }
        public PeriodTypeEnum MaxPeriodType { get; set; }
        public bool IsInterval { get; set; }
        public Guid SingleDepositID { get; set; }
        public Guid SingleCreditID { get; set; }
        public SingleDeposit SingleDeposit { get; set; }
        public SingleCredit SingleCredit { get; set; }
    }
}
