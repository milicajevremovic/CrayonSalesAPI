using Crayon.Infrastructure.Common;
using Crayon.Infrastructure.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Crayon.Crayon.Licences.Commands
{
    public record CancelSoftwareLicenseCommand(Guid PurchasedSoftwareId)
        : IRequest<Result<bool>>;

    public class CancelSoftwareLicenseCommandHandler : IRequestHandler<CancelSoftwareLicenseCommand, Result<bool>>
    {
        private readonly IPurchasedSoftwareRepository _purchasedSoftwareRepository;

        public CancelSoftwareLicenseCommandHandler(IPurchasedSoftwareRepository purchasedSoftwareRepository) =>
            _purchasedSoftwareRepository = purchasedSoftwareRepository;

        public async Task<Result<bool>> Handle(CancelSoftwareLicenseCommand request, CancellationToken cancellationToken)
        {
            var purchasedSoftware = await _purchasedSoftwareRepository.GetByIdAsync(request.PurchasedSoftwareId);
            if (purchasedSoftware == null)
                return Result<bool>.Failure("Purchased software not found", 404);

            purchasedSoftware.State = "Canceled";
            await _purchasedSoftwareRepository.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
