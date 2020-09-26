using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;

namespace FinancialCabinet.Model
{
    public class UserModel : IUser
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public List<DepositModel> DepositList { get; set; }
    }
}
