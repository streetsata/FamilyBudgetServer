using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models.DataTransferObjects
{
    public class AccountDTO
    {
        public Guid AccountID { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public DateTime BalanceStartDate { get; set; }
    }
}
