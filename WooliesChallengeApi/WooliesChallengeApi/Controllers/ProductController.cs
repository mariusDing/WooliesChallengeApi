using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WooliesChallengeApi.Application.Enum;
using WooliesChallengeApi.Application.Products.Queries;
using WooliesChallengeApi.Application.Users.Queries;

namespace WooliesChallengeApi.Controllers
{
    public class ProductController : BaseController
    {
        // GET api/answers/sort
        [HttpGet("sort")]
        public async Task<ActionResult> GetSortedProducts(string sortOption)
        {
            if (Enum.TryParse(sortOption, true, out SortOptions sortOptionEnum))
            {
                var query = new GetProductsQuery() { SortOption = sortOptionEnum };

                var sortedProduct = await Mediator.Send(query);

                return Ok(sortedProduct);
            }
            else
            {
                return new BadRequestObjectResult("This sort option does not support.");
            }
        }
    }
}
