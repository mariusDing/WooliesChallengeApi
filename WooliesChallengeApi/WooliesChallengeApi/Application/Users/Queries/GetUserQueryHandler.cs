using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WooliesChallengeApi.ViewModels;
using WooliesChallengeApi.Options;
using WooliesChallengeApi.Application.Users.Models;

namespace WooliesChallengeApi.Application.Users.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserVM>
    {
        private readonly IMapper _mapper;
        private readonly UserOption _userOption;

        public GetUserQueryHandler(IMapper mapper, IOptions<UserOption> userOption)
        {
            _mapper = mapper;
            _userOption = userOption.Value;
        }

        public async Task<UserVM> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            // In real app, user can be gotten by db context
            // Current user should be UserDTO. Just for simplify here.

            var user = new User()
            {
                Name = _userOption.Name,
                Token = _userOption.Token
            };

            return await Task.FromResult(_mapper.Map<UserVM>(user));
        }
    }
}
