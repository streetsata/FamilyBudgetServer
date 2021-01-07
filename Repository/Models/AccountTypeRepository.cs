using Contracts.Models;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class AccountTypeRepository : RepositoryBase<AccountType>, IAccountTypeRepository
    {
        public AccountTypeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<AccountType>> GetAllAccountTypesAsync()
        {
            return await FindAll()
                .OrderBy(at => at.Name)
                .ToListAsync();
        }

        public async Task<AccountType> GetAccountTypeByIdAsync(Guid accountTypeID)
        {
            return await FindByCondition(accountType => accountType.AccountTypeID.Equals(accountTypeID))
                .FirstOrDefaultAsync();
        }

        public async Task<AccountType> GetAccountTypeWithDetailsAsync(Guid accountTypeID)
        {
            return await FindByCondition(accountType => accountType.AccountTypeID.Equals(accountTypeID))
                .Include(ac => ac.Accounts)
                .FirstOrDefaultAsync();
        }

        public void CreateAccountType(AccountType accountType)
        {
            Create(accountType);
        }

        public void UpdateAccountType(AccountType accountType)
        {
            Update(accountType);
        }

        public void DeleteAccountType(AccountType accountType)
        {
            Delete(accountType);
        }
    }
}
