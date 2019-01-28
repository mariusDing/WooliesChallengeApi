using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WooliesChallengeApi.Application.Infrastructure.APIs
{
    public static class API
    {
        private const string Endpoint = "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource";

        private const string Procuts = "products";

        private const string ShopHistory = "shopperHistory";

        public static string GetProduct(string token)
        {
            return $"{Endpoint}/{Procuts}?{AppendQueryString(token)}";
        }

        public static string GetShopperHistory(string token)
        {
            return $"{Endpoint}/{ShopHistory}?{AppendQueryString(token)}";
        }

        private static string AppendQueryString(string token)
        {
            var query = HttpUtility.ParseQueryString($"token={token}");

            return query.ToString();
        }
    }
}
