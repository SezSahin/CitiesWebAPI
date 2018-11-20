using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesWebAPI.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository City { get; }
        IAttractionRepository Attraction { get; }
        int Complete();
    }
}
