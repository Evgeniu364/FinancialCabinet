using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Entity
{
    public class Phone: IPhone
    {
        public Guid ID { get; set; }
        public string PhoneNumber { get; set; }
        public Guid BankID { get; set; }
        public Bank Bank { get; set; }
    }
}
