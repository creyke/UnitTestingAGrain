using Moq;
using Orleans.Core;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;
using UnitTestingAGrain.Grains;
using UnitTestingAGrain.Interfaces;
using Xunit;

namespace UnitTestingAGrain.Tests.Grains
{
    public class MyGrainTests
    {
        private IMyGrain subject;

        private Mock<IGrainIdentity> mockGrainIdentity;
        private Mock<IGrainRuntime> mockGrainRuntime;

        public MyGrainTests()
        {
            mockGrainIdentity = new Mock<IGrainIdentity>();
            mockGrainRuntime = new Mock<IGrainRuntime>();

            subject = new MyGrain(
                mockGrainIdentity.Object, mockGrainRuntime.Object);
        }

        [Fact]
        public async Task CanCalcSquareRoot()
        {
            double x = 25;

            Assert.Equal(Math.Sqrt(x), await subject.SquareRoot(x));
        }
    }
}
