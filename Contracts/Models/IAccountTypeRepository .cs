using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public interface IAccountTypeRepository : IRepositoryBase<AccountType>
    {
        Task<IEnumerable<AccountType>> GetAllAccountTypesAsync();
        Task<AccountType> GetAccountTypeByIdAsync(Guid accountTypeID);
        Task<AccountType> GetAccountTypeWithDetailsAsync(Guid accountTypeID);
        void CreateAccountType(AccountType accountType);
        void UpdateAccountType(AccountType accountType);
        void DeleteAccountType(AccountType accountType);
    }
}
