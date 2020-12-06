using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FinancialCabinet.Entity
{
    public class User: IdentityUser<Guid>, IUser
    {
        public DateTime DateRegistration { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int GroupNumber { get; set; }
        public List<LikeDeposit> LikeDepositList { get; set; } 
        public List<LikeCredit> LikeCreditList { get; set; } 
        public Individual Individual { get; set; }
        public LegalEntity LegalEntity { get; set; }
        
        public Guid? IndividualID { get; set; }
        public Guid? LegalEntityID { get; set; }
        
    }
}
