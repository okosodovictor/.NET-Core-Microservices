using System.Collections.Generic;
using System.Linq;
using Actio.Services.Activities.Domain.Models;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Actio.Services.Activities.Domain.repositories;
using Actio.Services.Activities.Services.ServiceImpl;
using System;

namespace Actio.Api.Tests.Actio.Services.Activities.Tests.Unit.Services
{
   public class ActivityServiceTest
    {
        [Fact]
        public async Task acivivity_add_async_method_should_succeed()
        {
            //arrange
            var category = "work";
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(x => x.GetAsync(category))
                .ReturnsAsync(new Category(category));
            var activityService = new ActivityService(activityRepositoryMock.Object, categoryRepositoryMock.Object);
            var id = Guid.NewGuid();
            //Act
            await activityService.AddAsync(id, Guid.NewGuid(), category, "activity", "description", DateTime.UtcNow);
            //Assert
            categoryRepositoryMock.Verify(x => x.GetAsync(category), Times.AtLeastOnce);
            activityRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Activity>()), Times.AtLeastOnce);
        } 
    }
}
