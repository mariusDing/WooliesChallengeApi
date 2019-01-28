using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WooliesChallengeApi.Application.Users.Models;
using WooliesChallengeApi.Application.Users.Queries;
using WooliesChallengeApi.Options;
using WooliesChallengeApi.ViewModels;
using Xunit;

namespace WooliesChallengeApi.Test
{
    public class TestUserHandler
    {
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<IOptions<UserOption>>_mockUserOption = new Mock<IOptions<UserOption>>();
        private readonly CancellationToken _cancellationToken;

        public TestUserHandler()
        {
            _cancellationToken = new CancellationToken();

            _mockMapper.Setup(x => x.Map<UserVM>(It.IsAny<User>()))
                       .Returns((User source) =>
                        {
                            return new UserVM()
                            {
                                Name = source.Name,
                                Token = source.Token
                            };
                        });

            _mockUserOption.SetupGet(x => x.Value).Returns(new UserOption() { Name = "Marius Ding", Token = "" });
        }

        [Fact]
        public async void Should_GetUserQueryHandler_ReturnCorrectUserInfo()
        {
            // Arrange
            var query = new GetUserQuery();

            var handler = new GetUserQueryHandler(_mockMapper.Object, _mockUserOption.Object);

            var exptectedUserName = _mockUserOption.Object.Value.Name;

            // Action
            var result = await handler.Handle(query, _cancellationToken);

            // Assert
            Assert.Equal(result.Name, exptectedUserName);
        }
    }
}
