using Orleans;
using Orleans.Streams;
using System;
using System.Threading.Tasks;

namespace UnitTestingAGrain.Interfaces
{
    public interface IMyGrain : IAsyncObserver<int>, IGrainWithGuidKey
    {
        Task<bool> DoSomething();
        Task<bool> DoSomethingOnAnotherGrain(Guid anotherGrainId);
        Task<int> GetMessagesReceived();
        Task<Guid> GetPrimaryKey();
    }
}
