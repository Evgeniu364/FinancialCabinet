using FinancialCabinet.Interface;
using System;

namespace FinancialCabinet.Model
{
    public class UserModel : IUser
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
    }
}
