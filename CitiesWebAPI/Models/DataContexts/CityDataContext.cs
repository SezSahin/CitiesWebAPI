using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesWebAPI.Models.DataContexts
{
    public class CityDataContext : DbContext
    {
        public CityDataContext(DbContextOptions<CityDataContext> options) : base(options)
        {

        }

        public DbSet<City> Cities { get; set; }
    }
}
