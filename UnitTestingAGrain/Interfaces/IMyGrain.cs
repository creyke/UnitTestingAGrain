using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;

namespace UnitTestingAGrain.Interfaces
{
    public interface IMyGrain : IGrainWithGuidKey
    {
        Task<double> SquareRoot(double x);
    }
}
