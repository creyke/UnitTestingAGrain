using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using System;

namespace UnitTestingAGrain.Interfaces
{
    public interface IMyGrain : IGrainWithGuidKey
    {
        Task<Guid> GetPrimaryKey();
        Task<bool> DoSomethingOnAnotherGrain(Guid anotherGrainId);
        Task<bool> DoSomething();
    }
}
