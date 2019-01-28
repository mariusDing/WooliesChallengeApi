using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesChallengeApi.Application.Enum;
using WooliesChallengeApi.ViewModels;

namespace WooliesChallengeApi.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<List<ProductVM>>
    {
        public SortOptions? SortOption { get; set; }
    }
}
