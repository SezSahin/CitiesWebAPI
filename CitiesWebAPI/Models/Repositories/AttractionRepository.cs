using CitiesWebAPI.Interfaces;
using CitiesWebAPI.Models.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesWebAPI.Models.Repositories
{
    public class AttractionRepository : Repository<Attraction>, IAttractionRepository
    {
        public AttractionRepository(DataContext dataContext)
            : base(dataContext)
        {
        }

        public IEnumerable<Attraction> GetAttractionsByCity(int id)
        {
            return DataContext.Cities.Include(c => c.attractions).FirstOrDefault(c => c.Id == id).attractions.ToList();
        }

        public DataContext DataContext
        {
            get { return base.Context as DataContext; }
        }
    }
}
