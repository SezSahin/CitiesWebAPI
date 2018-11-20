using CitiesWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesWebAPI.Interfaces
{
    public interface IAttractionRepository : IRepository<Attraction>
    {
        IEnumerable<Attraction> GetAttractionsByCity(int id);
    }
}
