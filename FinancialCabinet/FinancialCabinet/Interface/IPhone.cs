using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Interface
{
    public interface IPhone
    {
        public Guid ID { get; set; }
        public string PhoneNumber { get; set; }
        public Guid BankID { get; set; }
    }
}
