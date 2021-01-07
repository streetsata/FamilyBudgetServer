using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Models
{
    public interface IAccountTypeRepository : IRepositoryBase<AccountType>
    {
        IEnumerable<AccountType> GetAllAccountTypes();
        AccountType GetAccountTypeById(Guid accountTypeID);
        AccountType GetAccountTypeWithDetails(Guid accountTypeID);

    }
}
