using System;
using FinancialCabinet.Interface;

namespace FinancialCabinet.Entity
{
    public class LegalEntity : ILegalEntity
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public int Unp { get; set; }
        public int NumberDocument { get; set; }
        public double CashTurnover { get; set; }
        public User User { get; set; }
        public Guid UserID { get; set; }
    }
}