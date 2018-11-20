using CitiesWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesWebAPI.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        IEnumerable<City> GetCitiesWithoutAttractions();
        IEnumerable<City> GetCitiesWithAttractions();
        City GetCityWithoutAttractions(int id);
        City GetCityWithAttractions(int id);
    }
}
