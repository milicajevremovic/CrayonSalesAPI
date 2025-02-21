using Crayon.Crayon.Domain;
using Crayon.Infrastructure.Common;
using Crayon.Infrastructure.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Crayon.Crayon.Licences.Commands
{
    public record OrderSoftwareLicenseCommand(Guid AccountId, Guid SoftwareServiceId, int Quantity, DateTime ValidTo)
    : IRequest<Result<PurchasedSoftwareDto>>;

    public class OrderSoftwareLicenseCommandHandler : IRequestHandler<OrderSoftwareLicenseCommand, Result<PurchasedSoftwareDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPurchasedSoftwareRepository _purchasedSoftwareRepository;

        public OrderSoftwareLicenseCommandHandler(IAccountRepository accountRepository, IPurchasedSoftwareRepository purchasedSoftwareRepository)
        {
            _accountRepository = accountRepository;
            _purchasedSoftwareRepository = purchasedSoftwareRepository;
        }

        public async Task<Result<PurchasedSoftwareDto>> Handle(OrderSoftwareLicenseCommand request, CancellationToken cancellationToken)
        {
            // validating account exists
            var account = await _accountRepository.GetAccountByIdAsync(request.AccountId);
            if (account == null)
                return Result<PurchasedSoftwareDto>.Failure("Account not found", 404);

            // simulating CCP API call – here we hardcoded software name based on the SoftwareServiceId
            string softwareName = "Auto CAD"; // for demo purposes

            var purchasedSoftware = new PurchasedSoftware
            {
                Id = Guid.NewGuid(),
                SoftwareName = softwareName,
                Quantity = request.Quantity,
                ValidTo = request.ValidTo,
                AccountId = account.Id,
                Account = account
            };

            await _purchasedSoftwareRepository.AddAsync(purchasedSoftware);
            await _purchasedSoftwareRepository.SaveChangesAsync();

            var dto = new PurchasedSoftwareDto(purchasedSoftware.Id, purchasedSoftware.SoftwareName, purchasedSoftware.Quantity, purchasedSoftware.State, purchasedSoftware.ValidTo);
            return Result<PurchasedSoftwareDto>.Success(dto, 201);
        }
    }
}
