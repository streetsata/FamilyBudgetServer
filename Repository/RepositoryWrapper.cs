using Contracts;
using Contracts.Models;
using Entities;
using Repository.Models;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IAccountTypeRepository _accountType;
        private IAccountRepository _account;
        public IAccountTypeRepository AccountType
        {
            get
            {
                if (_accountType == null)
                {
                    _accountType = new AccountTypeRepository(_repoContext);
                }
                return _accountType;
            }
        }
        public IAccountRepository Account
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountRepository(_repoContext);
                }
                return _account;
            }
        }
        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}
