using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WooliesChallengeApi.Application.Infrastructure;

namespace WooliesChallengeApi.Application.Trolleys
{
    public class CalculateTrolleyQueryHandler : IRequestHandler<CalculateTrolleyQuery, decimal>
    {
        private readonly IWooliesClient _client;

        public CalculateTrolleyQueryHandler(IWooliesClient wooliesClient)
        {
            _client = wooliesClient;
        }

        public async Task<decimal> Handle(CalculateTrolleyQuery request, CancellationToken cancellationToken)
        {
            return await _client.GetTotal(request);
        }
    }
}
