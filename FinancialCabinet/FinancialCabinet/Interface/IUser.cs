using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.Interface
{
    interface IUser
    {
        
        public DateTime DateRegistration { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        
    }
}
