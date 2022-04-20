using App.Common.Entity;
using App.Common.Repository;
using Microsoft.Xrm.Sdk;
using Moq;
using System;
using Xunit;


namespace Tests.Repository
{
    public class DynamicsTimeEntryRepositoryTests
    {
        [Fact]
        public void Should_Save()
        {
            var id = Guid.NewGuid();
            var organizationServiceMock = new Mock<IOrganizationService>();
            organizationServiceMock.Setup(x => x.Create(It.IsAny<Microsoft.Xrm.Sdk.Entity>()))
                .Returns(id);

            var repository = new DynamicsTimeEntryRepository(organizationServiceMock.Object);
            var result= repository.Save(new TimeEntry { Duration=1, Start=DateTime.Now, End=DateTime.Now });

            Assert.Equal(result, id);

        }

        [Fact]
        public void Should_Not_Save()
        {
            var organizationServiceMock = new Mock<IOrganizationService>();
            organizationServiceMock.Setup(x => x.Create(It.IsAny<Microsoft.Xrm.Sdk.Entity>()))
                .Throws(new Exception($"Connection failed."));

            var repository = new DynamicsTimeEntryRepository(organizationServiceMock.Object);

            Assert.Throws<Exception>(() => repository.Save(new TimeEntry { Duration = 1, Start = DateTime.Now, End = DateTime.Now }));

        }
    }
}
