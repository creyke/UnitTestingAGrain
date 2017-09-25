using Orleans;
using Orleans.Core;
using Orleans.Runtime;
using Orleans.Streams;
using System;
using System.Threading.Tasks;
using UnitTestingAGrain.Interfaces;

namespace UnitTestingAGrain.Grains
{
    public class MyGrain : Grain<int>, IMyGrain
    {
        public MyGrain(IGrainIdentity identity, IGrainRuntime runtime, IStorage<int> storage)
            : base(identity, runtime, storage)
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

        public Task<int> GetMessagesReceived()
        {
            return Task.FromResult(State);
        }

        public Task<Guid> GetPrimaryKey()
        {
            return Task.FromResult(GrainExtensions.GetPrimaryKey(this));
        }

        public Task OnCompletedAsync()
        {
            throw new NotImplementedException();
        }

        public Task OnErrorAsync(Exception ex)
        {
            throw new NotImplementedException();
        }

        public Task OnNextAsync(int item, StreamSequenceToken token = null)
        {
            State++;
            return Task.CompletedTask;
        }
    }
}
