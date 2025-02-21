using Crayon.Crayon.CCP;
using Crayon.Infrastructure.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Crayon.Crayon.SoftwareServices.Queries
{
    public record GetCCPSoftwareServicesQuery() : IRequest<Result<List<SoftwareServiceDto>>>;

    public class GetCCPSoftwareServicesQueryHandler : IRequestHandler<GetCCPSoftwareServicesQuery, Result<List<SoftwareServiceDto>>>
    {
        public Task<Result<List<SoftwareServiceDto>>> Handle(GetCCPSoftwareServicesQuery request, CancellationToken cancellationToken)
        {
            var services = new List<SoftwareServiceDto>
            {
                // As per request, we are not implemeting HTTP calls from CCP
                new(Guid.NewGuid(), "VS Code"),
                new (Guid.NewGuid(), "Docker"),
                new (Guid.NewGuid(), "Adobe Acrobat 365"),
                new (Guid.NewGuid(), "Auto CAD")
            };

            return Task.FromResult(Result<List<SoftwareServiceDto>>.Success(services));
        }
    }
}
