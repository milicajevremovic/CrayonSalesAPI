using Crayon.Crayon.Domain;
using Crayon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crayon.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;
        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Account>> GetAccountsAsync() =>
            await _context.Accounts.Include(a => a.PurchasedSoftwares).ToListAsync();

        public async Task<Account?> GetAccountByIdAsync(Guid accountId) =>
            await _context.Accounts.Include(a => a.PurchasedSoftwares)
                                   .FirstOrDefaultAsync(a => a.Id == accountId);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
