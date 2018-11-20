using CitiesWebAPI.Interfaces;
using CitiesWebAPI.Models.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace CitiesWebAPI.Models.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(DataContext context)
            : base(context)
        {
            
        }

        public IEnumerable<City> GetCitiesWithoutAttractions()
        {
            return DataContext.Cities.ToList();
        }

        public IEnumerable<City> GetCitiesWithAttractions()
        {
            return DataContext.Cities.Include(c => c.attractions).ToList();
        }

        public City GetCityWithoutAttractions(int id)
        {
            return DataContext.Cities.FirstOrDefault(c => c.Id == id);
        }

        public City GetCityWithAttractions(int id)
        {
            return DataContext.Cities.Include(c => c.attractions).FirstOrDefault(c => c.Id == id);
        }

        public DataContext DataContext
        {
            get { return base.Context as DataContext; }
        }
    }
}
