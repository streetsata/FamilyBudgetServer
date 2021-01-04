using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("account")]
    public class Account
    {
        public Guid AccountID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Balance is required")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "Balance start date is required")]
        [DataType(DataType.Date)]
        public DateTime BalanceStartDate { get; set; }

        [ForeignKey(nameof(AccountType))]
        public Guid AccountTypeID { get; set; }
        public AccountType AccountType { get; set; }
    }
}
