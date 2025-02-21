using Crayon.Infrastructure.Common;
using Crayon.Infrastructure.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Crayon.Crayon.Licences.Commands
{
    public record ChangeLicenseQuantityCommand(Guid PurchasedSoftwareId, int NewQuantity)
       : IRequest<Result<PurchasedSoftwareDto>>;

    public class ChangeLicenseQuantityCommandHandler : IRequestHandler<ChangeLicenseQuantityCommand, Result<PurchasedSoftwareDto>>
    {
        private readonly IPurchasedSoftwareRepository _purchasedSoftwareRepository;

        public ChangeLicenseQuantityCommandHandler(IPurchasedSoftwareRepository purchasedSoftwareRepository) =>
            _purchasedSoftwareRepository = purchasedSoftwareRepository;

        public async Task<Result<PurchasedSoftwareDto>> Handle(ChangeLicenseQuantityCommand request, CancellationToken cancellationToken)
        {
            var purchasedSoftware = await _purchasedSoftwareRepository.GetByIdAsync(request.PurchasedSoftwareId);
            if (purchasedSoftware == null)
                return Result<PurchasedSoftwareDto>.Failure("Purchased software not found", 404);

            purchasedSoftware.Quantity = request.NewQuantity;
            await _purchasedSoftwareRepository.SaveChangesAsync();

            var dto = new PurchasedSoftwareDto(purchasedSoftware.Id, purchasedSoftware.SoftwareName, purchasedSoftware.Quantity, purchasedSoftware.State, purchasedSoftware.ValidTo);
            return Result<PurchasedSoftwareDto>.Success(dto);
        }
    }
}
