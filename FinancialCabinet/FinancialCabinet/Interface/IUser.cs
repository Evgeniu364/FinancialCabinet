using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.Interface
{
    interface IUser
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
    }
}
