using Crayon.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Crayon.Infrastructure.Common;

namespace Crayon.Crayon.Accounts.Queries
{
    public record GetAccountsQuery() : IRequest<Result<List<AccountDto>>>;

    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, Result<List<AccountDto>>>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAccountsQueryHandler(IAccountRepository accountRepository) =>
            _accountRepository = accountRepository;

        public async Task<Result<List<AccountDto>>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.GetAccountsAsync();
            var accountDtos = accounts.Select(a => new AccountDto(a.Id, a.Name)).ToList();
            return Result<List<AccountDto>>.Success(accountDtos);
        }
    }
}
