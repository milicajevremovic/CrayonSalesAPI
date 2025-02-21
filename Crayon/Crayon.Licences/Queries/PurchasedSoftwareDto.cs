using System;

namespace Crayon.Crayon.Licences.Queries
{
    public record PurchasedSoftwareDto(Guid Id, string SoftwareName, int Quantity, string State, DateTime ValidTo);
}
