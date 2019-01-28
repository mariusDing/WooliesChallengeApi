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
using WooliesChallengeApi.Application.Trolleys;
using System.Text;

namespace WooliesChallengeApi.Application.Infrastructure
{
    public class WooliesClient : IWooliesClient
    {
        private readonly HttpClient _httpClient;
        private readonly UserOption _userOption;

        public WooliesClient(HttpClient httpClient, IOptions<UserOption> userOption)
        {
            _httpClient = httpClient;
            _userOption = userOption.Value;
        }

        public async Task<List<Product>> GetProducts()
        {
            var uri = API.GetProduct(_userOption.Token);

            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Get products fails. Msg:{response.ReasonPhrase}");

            var products = JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync());

            return products;
        }

        public async Task<List<ShopperHistory>> GetShopHistories()
        {
            var uri = API.GetShopperHistory(_userOption.Token);

            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Get shopper history fails. Msg:{response.ReasonPhrase}");

            var shopperHistories = JsonConvert.DeserializeObject<List<ShopperHistory>>(await response.Content.ReadAsStringAsync());

            return shopperHistories;
        }

        public async Task<decimal> GetTotal(CalculateTrolleyQuery request)
        {
            var uri = API.GetTotal(_userOption.Token);

            var response = await _httpClient.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Post to calculate trolley fails. Msg:{response.ReasonPhrase}");

            var total = Convert.ToDecimal(await response.Content.ReadAsStringAsync());

            return total;
        }
    }
}
