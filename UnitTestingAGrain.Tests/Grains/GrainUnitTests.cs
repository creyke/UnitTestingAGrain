using Moq;
using Orleans;
using Orleans.Core;
using Orleans.Runtime;
using System;

namespace UnitTestingAGrain.Tests.Grains
{
    public abstract class GrainUnitTests<TInterface, TGrain>
        where TInterface : IGrain
        where TGrain : Grain, TInterface
    {
        protected TInterface Subject { get; private set; }

        protected Mock<IGrainFactory> MockGrainFactory { get; private set; }
        protected Mock<IGrainIdentity> MockGrainIdentity { get; private set; }
        protected Mock<IGrainRuntime> MockGrainRuntime { get; private set; }

        public GrainUnitTests()
        {
            MockGrainIdentity = new Mock<IGrainIdentity>();
            MockGrainRuntime = new Mock<IGrainRuntime>();
            MockGrainFactory = new Mock<IGrainFactory>();

            MockGrainRuntime.Setup(x => x.GrainFactory).Returns(MockGrainFactory.Object);
            
            Subject = (TInterface)Activator.CreateInstance(typeof(TGrain),
                new object[] { MockGrainIdentity.Object, MockGrainRuntime.Object });
        }
    }
}
