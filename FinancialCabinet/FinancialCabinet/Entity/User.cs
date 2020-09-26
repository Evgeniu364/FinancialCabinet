using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;

namespace FinancialCabinet.Entity
{
    public class User: IUser
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public List<LikeDeposit> LikeDepositList { get; set; } 
    }
}
