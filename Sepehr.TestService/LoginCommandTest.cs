using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Sepehr.Application.Features.Login;
using Sepehr.Application.Helpers;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;

namespace Sepehr.TestService
{
    public class LoginCommandTest
    {
        private readonly Mock<IApplicationUserRepositoryAsync> _userRepositoryMock;
        private readonly Mock<IPasswordHelper> _passwordHelper;
        private readonly Mock<IDistributedCache> _distributeCache;
        private readonly Mock<IJwtProvider> _jwt;
        private readonly Mock<IMapper> _mapperMock;


        public LoginCommandTest()
        {
            _userRepositoryMock = new();
            _passwordHelper = new();
            _distributeCache = new();
            _jwt = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnOk()
        {
            Fixture fixutre = new Fixture();
            //Arrange
            var command = new LoginCommand
            {
                UserName = "sepehruser",
                Password = "Sep@u123456",
                captchaCode = "",
                captchaKey = ""
            };

            var handler = new LoginCommandHandler(
                _userRepositoryMock.Object,
                _distributeCache.Object,
                _passwordHelper.Object,
                _jwt.Object,
                _mapperMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Succeeded);
            //Assert.Equal();

        }
    }
}
