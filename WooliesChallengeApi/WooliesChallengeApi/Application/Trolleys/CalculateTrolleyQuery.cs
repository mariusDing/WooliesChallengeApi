using MediatR;
using System.Collections.Generic;
using WooliesChallengeApi.Application.Products.Model;
using WooliesChallengeApi.Application.Trolleys.Model;

namespace WooliesChallengeApi.Application.Trolleys
{
    public class CalculateTrolleyQuery : IRequest<double>
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public List<Special> Specials { get; set; } = new List<Special>();

        public List<QuantityValue> Quantities { get; set; } = new List<QuantityValue>();
    }
}
