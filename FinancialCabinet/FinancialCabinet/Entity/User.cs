using FinancialCabinet.Interface;
using System;

namespace FinancialCabinet.Entity
{
    public class User: IUser
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
    }
}
