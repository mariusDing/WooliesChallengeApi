using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesChallengeApi.Application.Products.Model;

namespace WooliesChallengeApi.Application.ShopHistories.Model
{
    public class ShopperHistory
    {
        public int CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
