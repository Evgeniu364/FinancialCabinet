using System;
using FinancialCabinet.Entity;

namespace FinancialCabinet.Interface
{
    public interface IIndividual
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TypeDocument { get; set; }
        public string NumberDocument { get; set; }
        public double? Salary { get; set; }
        public User User { get; set; }
        public Guid UserID { get; set; }
    }
}