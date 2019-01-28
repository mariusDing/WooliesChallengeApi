using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WooliesChallengeApi.ViewModels;
using WooliesChallengeApi.Application.Infrastructure;
using WooliesChallengeApi.Application.Enum;
using WooliesChallengeApi.Application.Products.Model;

namespace WooliesChallengeApi.Application.Products.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductVM>>
    {
        private readonly IMapper _mapper;
        private readonly IWooliesClient _client;

        public GetProductsQueryHandler(IMapper mapper, IWooliesClient wooliesClient)
        {
            _mapper = mapper;
            _client = wooliesClient;
        }

        public async Task<List<ProductVM>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _client.GetProducts();

            if(request.SortOption.HasValue)
            {
                products = await SortProducts(request.SortOption.Value, products);
            }

            return _mapper.Map<List<ProductVM>>(products);
        }

        private async Task<List<Product>> SortProducts(SortOptions sortOptions, List<Product> products)
        {
            switch (sortOptions)
            {
                case SortOptions.Low:
                    return products.OrderBy(x => x.Price).ToList();

                case SortOptions.High:
                    return products.OrderByDescending(x => x.Price).ToList();

                case SortOptions.Descending:
                    return products.OrderByDescending(x => x.Name).ToList();

                case SortOptions.Ascending:
                    return products.OrderBy(x => x.Name).ToList();

                case SortOptions.Recommended:
                    var shopperHistories = await _client.GetShopHistories();

                    var productDict = shopperHistories.SelectMany(x => x.Products)
                                           .GroupBy(x => (x.Name))
                                           .Select(x => new Product()
                                           {
                                               Name = x.First().Name,
                                               Price = x.First().Price,
                                               Quantity = x.Sum(p => p.Quantity)
                                           })
                                           .OrderByDescending(x => x.Quantity)
                                           .ToDictionary(x => x.Name);

                    return GetProductsBasedOnPopularity(productDict, products);

                default:
                    return products;
            }
        }

        private List<Product> GetProductsBasedOnPopularity(Dictionary<string, Product> productDict, List<Product> products)
        {
            var sortedList = new List<Product>();

            foreach(var kvp in productDict)
            {
                sortedList.Add(products.Where(p => p.Name == kvp.Key).First());
            }

            foreach(var product in products)
            {
                if(!sortedList.Any(s => s.Name.Contains(product.Name)))
                {
                    sortedList.Add(product);
                }
            }

            return sortedList;
        } 
    }
}
