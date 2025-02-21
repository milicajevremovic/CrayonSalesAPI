using Crayon.Crayon.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crayon.Infrastructure.Repositories
{
    public interface IPurchasedSoftwareRepository
    {
        Task<PurchasedSoftware?> GetByIdAsync(Guid id);
        Task AddAsync(PurchasedSoftware purchasedSoftware);
        Task SaveChangesAsync();
    }
}
