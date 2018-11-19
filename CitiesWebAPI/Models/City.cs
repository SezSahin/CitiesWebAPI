using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesWebAPI.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        public List<Attraction> attractions { get; set; }
    }
}
