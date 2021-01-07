using Contracts.Models;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Models
{
    public class AccountTypeRepository : RepositoryBase<AccountType>, IAccountTypeRepository
    {
        public AccountTypeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<AccountType> GetAllAccountTypes()
        {
            return FindAll()
                .OrderBy(at => at.Name)
                .ToList();
        }

        public AccountType GetAccountTypeById(Guid accountTypeID)
        {
            return FindByCondition(accountType => accountType.AccountTypeID.Equals(accountTypeID))
                .FirstOrDefault();
        }

        public AccountType GetAccountTypeWithDetails(Guid accountTypeID)
        {
            return FindByCondition(accountType => accountType.AccountTypeID.Equals(accountTypeID))
                .Include(ac => ac.Accounts)
                .FirstOrDefault();
        }
    }
}
