using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FinancialCabinet.Entity
{
    public class User: IdentityUser, IUser
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public List<LikeDeposit> LikeDepositList { get; set; } 
    }
}
