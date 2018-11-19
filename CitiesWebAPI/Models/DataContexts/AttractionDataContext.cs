using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesWebAPI.Models.DataContexts
{
    public class AttractionDataContext : DbContext
    {
        public AttractionDataContext(DbContextOptions<AttractionDataContext> options) : base(options)
        {

        }

        public DbSet<Attraction> Attractions { get; set; }
    }
}
