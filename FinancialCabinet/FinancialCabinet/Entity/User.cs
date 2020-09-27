using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FinancialCabinet.Entity
{
    public class User: IdentityUser, IUser
    {
        public Guid ID { get; set; }
        public DateTime DateRegistration { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<LikeDeposit> LikeDepositList { get; set; } 
    }
}
