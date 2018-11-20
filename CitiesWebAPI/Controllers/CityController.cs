using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CitiesWebAPI.DTOs;
using CitiesWebAPI.Models;
using CitiesWebAPI.Models.DataContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CitiesWebAPI.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        public CityController(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        #region swagger
        /// <summary>
        /// Gets all City items.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /City
        ///     {
        ///        "id": 1,
        ///        "name": "City1",
        ///        "Description": A City
        ///     },
        ///     
        ///     {
        ///        "id": 2,
        ///        "name": "City2",
        ///        "Description": A City
        ///     }
        ///
        /// </remarks>
        /// <returns>All City items</returns>
        /// <response code="202">Returns the city items</response>
        /// <response code="400">If the city items are null</response>    
        #endregion
        [HttpGet]
        public ActionResult GetCities(bool getAttraction = false)
        {
            //cities.Add(new City() { Id = 1, Name = "Odense", Description = "Beautiful city" });
            //cities.Add(new City() { Id = 2, Name = "Copenhagen", Description = "Beautiful, but crowded city" });

            if (!getAttraction)
            {
                return new ObjectResult(_mapper.Map<List<CityOnlyDataTransferObject>>(_unitOfWork.City.GetCitiesWithoutAttractions()));
            }
            return new ObjectResult(_mapper.Map<List<CityDataTransferObject>>(_unitOfWork.City.GetCitiesWithAttractions())); // This will map from City to CityDataTransferObject
        }

        #region swagger
        /// <summary>
        /// Gets a City item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /City
        ///     {
        ///        "id": 1,
        ///        "name": "City",
        ///        "Description": A City
        ///     }
        ///
        /// </remarks>
        /// <returns>A City item equivalent to id parameter</returns>
        /// <response code="202">Returns the city item</response>
        /// <response code="400">If the city item is null</response>
        #endregion
        [HttpGet("{id}", Name ="GetCity")]
        public ActionResult<City> GetCity(int id, bool getAttraction = false)
        {
            //cities.Add(new City() { Id = 1, Name = "Odense", Description = "Beautiful city" });
            //cities.Add(new City() { Id = 2, Name = "Copenhagen", Description = "Beautiful, but crowded city" });

            if (_unitOfWork.City.Get(id) is null)
            {
                return NotFound();
            }
            if (!getAttraction)
            {
                return new ObjectResult(_mapper.Map<List<CityOnlyDataTransferObject>>(_unitOfWork.City.GetCityWithoutAttractions(id)));
            }
            return new ObjectResult(_mapper.Map<List<CityDataTransferObject>>(_unitOfWork.City.GetCityWithAttractions(id)));
        }

        #region swagger
        /// <summary>
        /// Creates a City item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /City
        ///     {
        ///        "id": 1,
        ///        "name": "City",
        ///        "Description": A City
        ///     }
        ///
        /// </remarks>
        /// /// <param name="city"></param>
        /// <returns>A newly created City item</returns>
        /// <response code="201">Returns the newly created city</response>
        /// <response code="400">If the city item is null</response>            
        #endregion
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult PostCity(City city)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //cities.Add(city);

            _unitOfWork.City.Add(city);
            _unitOfWork.Complete();

            return CreatedAtAction("PostCity", city);
        }

        #region swagger
        /// <summary>
        /// Edits a specific City item.
        /// </summary>
        #endregion
        [HttpPut("{id}")]
        public IActionResult Put(City city)
        {
            //for(int i = 0; i < cities.Count; i++)
            //{
            //    if (cities[i].Id == id)
            //    {
            //        cities[i] = city;
            //        return Ok();
            //    }
            //}
            //return NotFound();

            if (_unitOfWork.City.Get(city.Id) is null)
            {
                return NotFound();
            }

            City cityItem = _unitOfWork.City.Get(city.Id);

            try
            {
                cityItem.Name = city.Name;
                cityItem.Description = city.Description;

                _unitOfWork.Complete();

                return Ok();
            }
            catch (Exception)
            {
                return Conflict();
            }
        }

        [HttpPatch]
        [Route("Update/{id}")]
        public IActionResult Patch(JsonPatchDocument<City> cityPatch, int id)
        {
            var cityItem = _unitOfWork.City.Get(id);

            if (cityItem is null)
            {
                return NotFound();
            }
            try
            {
                cityPatch.ApplyTo(cityItem);

                _unitOfWork.Complete();

                return Ok();
            }
            catch (Exception)
            {
                return Conflict();
            }
        }

        #region swagger
        /// <summary>
        /// Deletes a specific City item.
        /// </summary>
        /// <param name="id"></param>
        #endregion
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //City city = cities.Where(x => x.Id == id).SingleOrDefault<City>();
            //cities.Remove(city);
            //return NoContent();

            var cityItem = _unitOfWork.City.Get(id);
            if (cityItem == null)
            {
                return NotFound();
            }

            try
            {
                _unitOfWork.City.Remove(cityItem);
                _unitOfWork.Complete();

                return NoContent();
            }
            catch(Exception)
            {
                return Conflict();
            }
        }

        //[ValidateModel]
        //[HttpPost("api/city/token")]
        //public IActionResult CreateToken([FromBody]City city)
        //{
        //    try
        //    {

        //    }
        //    catch(Exception ex)
        //    {
        //        return 
        //    }
        //}
    }
}