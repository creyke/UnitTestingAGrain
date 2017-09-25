using Moq;
using System;
using System.Threading.Tasks;
using UnitTestingAGrain.Grains;
using UnitTestingAGrain.Interfaces;
using Xunit;

namespace UnitTestingAGrain.Tests.Grains
{
    public class MyGrainTests : GrainUnitTests<IMyGrain, MyGrain, int>
    {
        [Fact]
        public async Task CanMockPrimaryKey()
        {
            var guid = Guid.Parse("00000001-0001-0001-0001-000000000001");
            MockGrainIdentity.Setup(x => x.PrimaryKey).Returns(guid);

            Assert.Equal(guid, await Subject.GetPrimaryKey());
        }

        [Fact]
        public async Task CanMockADependentGrain()
        {
            var guid = Guid.Empty;
            var mockGrain = new Mock<IMyGrain>();
            mockGrain.Setup(x => x.DoSomething()).ReturnsAsync(true);
            MockGrainFactory
                .Setup(x => x.GetGrain<IMyGrain>(guid, null))
                .Returns(mockGrain.Object);

            Assert.True(await Subject.DoSomethingOnAnotherGrain(guid));
        }

        [Fact]
        public async Task CanManipulateState()
        {
            var count = 10;
            for (int i = 0; i < count; i++)
            {
                await Subject.OnNextAsync(i);
            }

            Assert.Equal(count, await Subject.GetMessagesReceived());
        }
    }
}
