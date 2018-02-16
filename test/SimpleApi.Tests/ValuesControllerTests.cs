using System.Threading.Tasks;
using SimpleApi.Persistence;
using Xunit;
using Moq;
using SimpleApi.Controllers;
using System.Collections.Generic;
using SimpleApi.Models;
using System.Linq;
using FluentAssertions;

namespace SimpleApi.Tests
{
    public class ValuesControllerTests
    {
        [Fact]
        public void TestValuesGetAll()
        {
            // arrange
            var getAllList = new List<ValueModel>{
                    new ValueModel{
                        Id = 0,
                        Value = "First"
                    },
                    new ValueModel{
                        Id = 1,
                        Value = "Second"
                    }
                };
            var valueRepositoryMock = new Mock<IValueRepository>();
            valueRepositoryMock.Setup(m => m.GetAll()).Returns(getAllList.AsQueryable());         
            var sut = new ValuesController(valueRepositoryMock.Object);

            // act
            var result = sut.Get();

            // assert
            result.Should().BeEquivalentTo(getAllList);
            valueRepositoryMock.Verify(m => m.GetAll(), Times.Once);
        }
    }
}
