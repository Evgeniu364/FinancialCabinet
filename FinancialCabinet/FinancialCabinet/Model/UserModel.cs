using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinancialCabinet.Model
{
    public class UserModel
    {
        
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<DepositModel> DepositList { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        
        
    }
}
