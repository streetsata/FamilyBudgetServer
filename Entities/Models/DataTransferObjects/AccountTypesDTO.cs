using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models.DataTransferObjects
{
    public class AccountTypesDTO
    {
        public Guid AccountTypeID { get; set; }
        public string Name { get; set; }

        public IEnumerable<AccountDTO> Accounts { get; set; }
    }
}
