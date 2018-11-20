using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesWebAPI.DTOs
{
    public class AttractionDataTransferObject
    {
        public int id { get; set; }
        public int cityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
