﻿using FinancialCabinet.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCabinet.Entity
{
    public class LikeCredit : ILikeCredit
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid SingleCreditId { get; set; }
        public User User { get; set; }
        public SingleCredit SingleCredit { get; set; }
    }
}
