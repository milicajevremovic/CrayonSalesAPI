using System;

namespace Crayon.Crayon.Licences
{
    public record PurchasedSoftwareDto(Guid Id, string SoftwareName, int Quantity, string State, DateTime ValidTo);
}
