using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WooliesChallengeApi.Application.Infrastructure;

namespace WooliesChallengeApi.Application.Trolleys
{
    public class CalculateTrolleyQueryHandler : IRequestHandler<CalculateTrolleyQuery, double>
    {
        private readonly IWooliesClient _client;

        public CalculateTrolleyQueryHandler(IWooliesClient wooliesClient)
        {
            _client = wooliesClient;
        }

        public async Task<double> Handle(CalculateTrolleyQuery request, CancellationToken cancellationToken)
        {
            return await _client.GetTotal(request);
        }
    }
}
