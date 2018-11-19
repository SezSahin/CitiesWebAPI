using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesWebAPI.Models
{
    public class Attraction
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        [Required(ErrorMessage = "Too short")]
        [MinLength(1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Too short")]
        [MinLength(5)]
        public string Description { get; set; }
    }
}
