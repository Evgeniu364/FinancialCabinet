using FinancialCabinet.Infrastructure.Enum;
using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public int CompareTo(IPeriod period)
        {
            if (!MinPeriod.HasValue && period == null)
                return 0;
            if (!MinPeriod.HasValue)
                return -1;
            if (period == null)
                return 1;
            if (MinPeriod.Value < period.MinPeriod)
                return -1;
            if (MinPeriod.Value > period.MinPeriod)
                return 1;
            return 0;
        }
    }
}
