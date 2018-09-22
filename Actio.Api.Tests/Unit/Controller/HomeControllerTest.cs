using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Actio.Api.Tests.Unit.Controller
{
    public class HomeControllerTest
    {
        [Fact]
        public void home_controller_get_should_return_string_content()
        {
            //Arrange
            var controller = new Controllers.HomeController();
            //Act
            var result  = controller.Get();
            var contentResult = result as ContentResult;
            //assert
            contentResult.Should().NotBeNull();
           //assert
            contentResult.Content.Should().Be("Hello from Actio Api");
        }
    }
}
