using Contracts;
using Contracts.Models;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Account> AccountsByAccountType(Guid accountTypeId)
        {
            return FindByCondition(ac => ac.AccountTypeID.Equals(accountTypeId));
        }

        
    }
}
