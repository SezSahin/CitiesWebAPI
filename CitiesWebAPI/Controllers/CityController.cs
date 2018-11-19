using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesWebAPI.Models;
using CitiesWebAPI.Models.DataContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitiesWebAPI.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        //City city = new City();

        public static List<City> cities = new List<City>();
        //{
        //    new City() { Id = 1, Name = "Odense", Description = "Beautiful city", attractions = new List<Attraction>{new Attraction{ Name = "Hotel", Description = "White" } } }
        //};

        //public static List<Attraction> attractions = new List<Attraction>();

        private readonly DataContext _context;
        public CityController(DataContext context)
        {
            _context = context;

            if(_context.Cities.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Cities.Add(new City{ Name = "City" });
                _context.SaveChanges();
            }
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
        public ActionResult<List<City>> GetCities()
        {
            //cities.Add(new City() { Id = 1, Name = "Odense", Description = "Beautiful city" });
            //cities.Add(new City() { Id = 2, Name = "Copenhagen", Description = "Beautiful, but crowded city" });
            var cityList = _context.Cities.ToList();

            foreach(City city in cityList)
            {
                city.attractions = _context.Attractions.Where(x => x.CityId == city.Id).ToList();
            }
            return Ok(cityList);
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
        public ActionResult<City> GetCity(int id)
        {
            //cities.Add(new City() { Id = 1, Name = "Odense", Description = "Beautiful city" });
            //cities.Add(new City() { Id = 2, Name = "Copenhagen", Description = "Beautiful, but crowded city" });

            var cityList = _context.Cities.Find(id); //cities.SingleOrDefault(x => x.Id == id);
            if(cityList == null)
            {
                return NotFound();
            }
            cityList.attractions = _context.Attractions.Where(x => x.CityId == id).ToList();
            return cityList;
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

            _context.Cities.Add(city);
            _context.SaveChanges();

            return CreatedAtAction("GetCity", new { id = city.Id }, city);
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

            var cityItem = _context.Cities.Find(city.Id);
            if (cityItem == null)
            {
                return NotFound();
            }

            cityItem.Description = city.Description;
            cityItem.Name = city.Name;

            _context.Cities.Update(cityItem);
            _context.SaveChanges();

            return NoContent();
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

            var cityItem = _context.Cities.Find(id);
            if (cityItem == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(cityItem);
            _context.SaveChanges();

            return NoContent();
        }

        [ValidateModel]
        [HttpPost("api/city/token")]
        public IActionResult CreateToken([FromBody]City city)
        {
            try
            {

            }
            catch(Exception ex)
            {
                return 
            }
        }
    }
}