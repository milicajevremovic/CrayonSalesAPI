using System;

namespace Crayon.Crayon.Domain
{
    public record PurchasedSoftware
    {
        public Guid Id { get; init; }
        public string SoftwareName { get; init; } = string.Empty;
        public int Quantity { get; set; }
        public string State { get; set; } = "Active";
        public DateTime ValidTo { get; set; }
        public Guid AccountId { get; init; }
        public Account Account { get; set; } = null!;
    }
}
