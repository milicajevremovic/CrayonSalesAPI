using System.Collections.Generic;

namespace Crayon.Crayon.Domain
{
    public record Account
    {
        public System.Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public List<PurchasedSoftware> PurchasedSoftwares { get; init; } = new();
    }
}
