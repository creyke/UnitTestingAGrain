using System.Threading.Tasks;
using Orleans;
using Orleans.Core;
using Orleans.Runtime;
using UnitTestingAGrain.Interfaces;
using System;

namespace UnitTestingAGrain.Grains
{
    public class MyGrain : Grain, IMyGrain
    {
        public MyGrain(IGrainIdentity identity, IGrainRuntime runtime)
            : base(identity, runtime)
        {
        }

        public Task<bool> DoSomething()
        {
            return Task.FromResult(true);
        }

        public async Task<bool> DoSomethingOnAnotherGrain(Guid anotherGrainId)
        {
            return await GrainFactory.GetGrain<IMyGrain>(anotherGrainId).DoSomething();
        }

        public Task<Guid> GetPrimaryKey()
        {
            return Task.FromResult(GrainExtensions.GetPrimaryKey(this));
        }
    }
}
