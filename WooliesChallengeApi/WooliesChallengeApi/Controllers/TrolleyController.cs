using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WooliesChallengeApi.Application.Trolleys;
using WooliesChallengeApi.Application.Users.Queries;

namespace WooliesChallengeApi.Controllers
{
    public class TrolleyController : BaseController
    {
        // GET api/answers/trolleyTotal
        [HttpPost("trolleyTotal")]
        public async Task<ActionResult> GetTotal(CalculateTrolleyQuery query)
        {
            var totalAmount = await Mediator.Send(query);

            return Ok(totalAmount);
        }
    }
}
