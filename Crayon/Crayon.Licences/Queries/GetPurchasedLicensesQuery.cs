using Crayon.Infrastructure.Common;
using Crayon.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crayon.Crayon.Licences.Queries
{
    public record GetPurchasedLicensesQuery(Guid AccountId) : IRequest<Result<List<PurchasedSoftwareDto>>>;

    public class GetPurchasedLicensesQueryHandler : IRequestHandler<GetPurchasedLicensesQuery, Result<List<PurchasedSoftwareDto>>>
    {
        private readonly IAccountRepository _accountRepository;
        public GetPurchasedLicensesQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Result<List<PurchasedSoftwareDto>>> Handle(GetPurchasedLicensesQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByIdAsync(request.AccountId);
            if (account == null)
                return Result<List<PurchasedSoftwareDto>>.Failure("Account not found", 404);

            var licenses = account.PurchasedSoftwares
                                  .Select(ps => new PurchasedSoftwareDto(ps.Id, ps.SoftwareName, ps.Quantity, ps.State, ps.ValidTo))
                                  .ToList();
            return Result<List<PurchasedSoftwareDto>>.Success(licenses);
        }
    }
}
