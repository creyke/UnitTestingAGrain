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

        public Task<double> SquareRoot(double x)
        {
            return Task.FromResult(Math.Sqrt(x));
        }
    }
}
