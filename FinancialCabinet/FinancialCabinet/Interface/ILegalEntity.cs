using System;
using FinancialCabinet.Entity;

namespace FinancialCabinet.Interface
{
    public interface ILegalEntity
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public int Unp { get; set; }
        public int NumberDocument { get; set; }
        public double CashTurnover { get; set; }
        public User User { get; set; }
    }
}