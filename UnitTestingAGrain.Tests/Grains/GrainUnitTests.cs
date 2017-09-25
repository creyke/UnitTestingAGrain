using Moq;
using Orleans;
using Orleans.Core;
using Orleans.Runtime;
using System;

namespace UnitTestingAGrain.Tests.Grains
{
    public abstract class GrainUnitTests<TInterface, TGrain, TGrainState>
        : GrainUnitTests<TInterface, TGrain>
        where TInterface : IGrain
        where TGrain : Grain<TGrainState>, TInterface
        where TGrainState : new()
    {
        protected TGrainState State { get; private set; }
        protected Mock<IStorage<TGrainState>> MockStorage { get; private set; }

        protected override void Setup()
        {
            base.Setup();
            MockStorage = new Mock<IStorage<TGrainState>>();
            MockStorage.Setup(x => x.State).Returns(() => { return State; });
            MockStorage.SetupSet(x => x.State = It.IsAny<TGrainState>()).Callback<TGrainState>(value => State = value);
        }

        protected override TInterface Instantiate()
        {
            return (TInterface)Activator.CreateInstance(typeof(TGrain),
                new object[] { MockGrainIdentity.Object, MockGrainRuntime.Object, MockStorage.Object });
        }
    }

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
            Setup();
            Subject = Instantiate();
        }

        protected virtual void Setup()
        {
            MockGrainIdentity = new Mock<IGrainIdentity>();
            MockGrainRuntime = new Mock<IGrainRuntime>();
            MockGrainFactory = new Mock<IGrainFactory>();

            MockGrainRuntime.Setup(x => x.GrainFactory).Returns(MockGrainFactory.Object);
        }

        protected virtual TInterface Instantiate()
        {
            return (TInterface)Activator.CreateInstance(typeof(TGrain),
                new object[] { MockGrainIdentity.Object, MockGrainRuntime.Object });
        }
    }
}
