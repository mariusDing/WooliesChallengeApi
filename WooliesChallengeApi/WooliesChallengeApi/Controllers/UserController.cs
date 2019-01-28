using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WooliesChallengeApi.Application.Users.Queries;

namespace WooliesChallengeApi.Controllers
{
    public class UserController : BaseController
    {
        // GET api/answers/user
        [HttpGet("user")]
        public async Task<ActionResult> Get()
        {
            var user = await Mediator.Send(new GetUserQuery());

            return Ok(user);
        }
    }
}
