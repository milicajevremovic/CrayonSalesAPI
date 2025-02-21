using Crayon.Crayon.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crayon.Infrastructure.Repositories
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAccountsAsync();
        Task<Account?> GetAccountByIdAsync(Guid accountId);
        Task SaveChangesAsync();
    }
}
