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
    public class PurchasedSoftwareRepository : IPurchasedSoftwareRepository
    {
        private readonly AppDbContext _context;
        public PurchasedSoftwareRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<PurchasedSoftware?> GetByIdAsync(Guid id) =>
            await _context.PurchasedSoftwares.FirstOrDefaultAsync(ps => ps.Id == id);

        public async Task AddAsync(PurchasedSoftware purchasedSoftware) =>
            await _context.PurchasedSoftwares.AddAsync(purchasedSoftware);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
