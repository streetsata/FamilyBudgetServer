using Contracts.Models;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class AccountTypeRepository : RepositoryBase<AccountType>, IAccountTypeRepository
    {
        public AccountTypeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
