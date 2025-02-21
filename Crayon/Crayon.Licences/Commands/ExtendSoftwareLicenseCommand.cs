using Crayon.Crayon.Licences.Queries;
using Crayon.Infrastructure.Common;
using Crayon.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crayon.Crayon.Licences.Commands
{
    public record ExtendSoftwareLicenseCommand(Guid PurchasedSoftwareId, DateTime NewValidTo)
        : IRequest<Result<PurchasedSoftwareDto>>;

    public class ExtendSoftwareLicenseCommandHandler : IRequestHandler<ExtendSoftwareLicenseCommand, Result<PurchasedSoftwareDto>>
    {
        private readonly IPurchasedSoftwareRepository _purchasedSoftwareRepository;

        public ExtendSoftwareLicenseCommandHandler(IPurchasedSoftwareRepository purchasedSoftwareRepository) =>
            _purchasedSoftwareRepository = purchasedSoftwareRepository;

        public async Task<Result<PurchasedSoftwareDto>> Handle(ExtendSoftwareLicenseCommand request, CancellationToken cancellationToken)
        {
            var purchasedSoftware = await _purchasedSoftwareRepository.GetByIdAsync(request.PurchasedSoftwareId);
            if (purchasedSoftware == null)
                return Result<PurchasedSoftwareDto>.Failure("Purchased software not found", 404);

            purchasedSoftware.ValidTo = request.NewValidTo;
            await _purchasedSoftwareRepository.SaveChangesAsync();

            var dto = new PurchasedSoftwareDto(purchasedSoftware.Id, purchasedSoftware.SoftwareName, purchasedSoftware.Quantity, purchasedSoftware.State, purchasedSoftware.ValidTo);
            return Result<PurchasedSoftwareDto>.Success(dto);
        }
    }
}
