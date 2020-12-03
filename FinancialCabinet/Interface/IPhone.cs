using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Interface
{
    public interface IPhone
    {
        public Guid ID { get; set; }

        [Phone(ErrorMessage = "Неверный формат номера")]
        public string PhoneNumber { get; set; }
        public Guid BankID { get; set; }
    }
}
