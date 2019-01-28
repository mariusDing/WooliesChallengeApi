using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesChallengeApi.ViewModels;

namespace WooliesChallengeApi.Application.Users.Queries
{
    public class GetUserQuery : IRequest<UserVM>
    {
    }
}
