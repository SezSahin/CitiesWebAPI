using CitiesWebAPI.Interfaces;
using CitiesWebAPI.Models.DataContexts;
using CitiesWebAPI.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesWebAPI.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
            City = new CityRepository(_context);
            Attraction = new AttractionRepository(_context);
        }

        public ICityRepository City { get; private set; }
        public IAttractionRepository Attraction { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
