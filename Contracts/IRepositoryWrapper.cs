using Contracts.Models;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IAccountTypeRepository AccountType { get; }
        IAccountRepository Account { get; }
        void Save();
    }
}
