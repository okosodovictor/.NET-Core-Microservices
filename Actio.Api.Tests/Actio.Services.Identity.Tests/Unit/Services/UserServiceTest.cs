using Actio.Common.Auth;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;
using Actio.Services.Identity.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Actio.Api.Tests.Actio.Services.Identity.Tests.Unit.Services
{
  public  class UserServiceTest
    {
        [Fact]
        public async Task user_service_login_should_return_jwt()
        {
            var email = "test@test.com";
            var password = "secret";
            var name = "name";
            var salt = "salt";
            var hash = "hash";
            var token = "token";

            var userRepositoryMock = new Mock<IUserRepository>();
            var encripterMock = new Mock<IEncripter>();
            var jwtHandleMock = new Mock<IJwtHandler>();
            encripterMock.Setup(x => x.GetSalt()).Returns(salt);
            encripterMock.Setup(x => x.GetHash(password, salt)).Returns(hash);
            jwtHandleMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(new JSonWebToken
            {
               Token = token
            });

            var user = new User(email, name);
            user.SetPassword(password, encripterMock.Object);
            userRepositoryMock.Setup(x => x.GetAsync(email)).ReturnsAsync(user);

            var userService = new UserService(userRepositoryMock.Object, encripterMock.Object, jwtHandleMock.Object);

            var jwt = await userService.LoginAsync(email, password);
            userRepositoryMock.Verify(x => x.GetAsync(email), Times.Once);

            jwtHandleMock.Verify(x => x.Create(It.IsAny<Guid>()), Times.Once);

            jwt.Should().NotBeNull();
            jwt.Token.Should().Be(token);
        }
    }
}
