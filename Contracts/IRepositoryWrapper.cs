using Contracts.Models;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IAccountTypeRepository AccountType { get; }
        IAccountRepository Account { get; }
        Task SaveAsync();
    }
}
