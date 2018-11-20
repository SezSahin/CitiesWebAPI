using CitiesWebAPI.DTOs;
using CitiesWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace CitiesWebAPI.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<City, CityOnlyDataTransferObject>();
            CreateMap<CityOnlyDataTransferObject, City>();
            CreateMap<City, CityDataTransferObject>();
            CreateMap<CityDataTransferObject, City>();
            CreateMap<Attraction, AttractionDataTransferObject>();
            CreateMap<AttractionDataTransferObject, Attraction>();
        }
    }
}
