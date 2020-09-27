using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;

namespace FinancialCabinet.Model
{
    public class UserModel : IUser
    {
        public Guid ID { get; set; }
        public DateTime DateRegistration { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<DepositModel> DepositList { get; set; }
    }
}
