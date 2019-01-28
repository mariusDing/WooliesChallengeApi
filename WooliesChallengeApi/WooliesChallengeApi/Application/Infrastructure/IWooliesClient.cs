using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WooliesChallengeApi.Application.Products.Model;
using WooliesChallengeApi.Application.Infrastructure.APIs;
using Microsoft.Extensions.Options;
using WooliesChallengeApi.Options;
using WooliesChallengeApi.Application.ShopHistories.Model;

namespace WooliesChallengeApi.Application.Infrastructure
{
    public interface IWooliesClient
    {
        Task<List<Product>> GetProducts();

        Task<List<ShopperHistory>> GetShopHistories();
    }
}
