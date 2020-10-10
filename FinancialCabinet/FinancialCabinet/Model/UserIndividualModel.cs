using System;

namespace FinancialCabinet.Model
{
    public class IndividualModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TypeDocument { get; set; }
        public string NumberDocument { get; set; }
        public double Salary { get; set; }
        
    }
}