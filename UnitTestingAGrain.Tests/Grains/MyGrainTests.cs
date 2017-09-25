using System;
using System.Threading.Tasks;
using UnitTestingAGrain.Grains;
using UnitTestingAGrain.Interfaces;
using Xunit;

namespace UnitTestingAGrain.Tests.Grains
{
    public class MyGrainTests : GrainUnitTests<IMyGrain, MyGrain>
    {
        [Fact]
        public async Task CanMockPrimaryKey()
        {
            var guid = Guid.Parse("00000001-0001-0001-0001-000000000001");
            MockGrainIdentity.Setup(x => x.PrimaryKey).Returns(guid);

            Assert.Equal(guid, await Subject.GetPrimaryKey());
        }

        [Fact]
        public async Task CanMockAnotherGrain()
        {
            var anotherGrainGuid = Guid.Empty;

            MockGrainFactory
                .Setup(x => x.GetGrain<IMyGrain>(anotherGrainGuid, null))
                .Returns(new MyGrain(MockGrainIdentity.Object, MockGrainRuntime.Object));

            Assert.True(await Subject.DoSomethingOnAnotherGrain(anotherGrainGuid));
        }
    }
}
