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
    public class AttractionController : ControllerBase
    {
        //public static List<Attraction> attractions = new List<Attraction>();

        //private List<City> _cities = new List<City>();
        ////{
        ////    new City { Id = 1, Name = "Odense", Description = "Beautiful city", attractions = new List<Attraction>{new Attraction{ Name = "Hotel", Description = "White" } } }
        ////};


        private readonly DataContext _context;

        public AttractionController(DataContext context)
        {
            _context = context;

            if (_context.Attractions.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Attractions.Add(new Attraction { Name = "Attraction", Description = "Nothing", CityId = 1 });
                _context.SaveChanges();
            }
        }

        
        #region swagger
        /// <summary>
        /// Gets all Attraction items.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Attractions
        ///     {
        ///        "id": 1,
        ///        "name": "Attraction1",
        ///        "Description": "An Attraction"
        ///     },
        ///     
        ///     {
        ///        "id": 2,
        ///        "name": "Attraction1",
        ///        "Description": "An Attraction"
        ///     }
        ///
        /// </remarks>
        /// <returns>All Attraction items</returns>
        /// <response code="202">Returns the attraction items</response>
        /// <response code="400">If the attraction items are null</response>
        #endregion
        [HttpGet(Name = "Attractions")]
        public ActionResult<List<Attraction>> GetAttractions()
        {

            var attr = _context.Attractions.ToList();

            return Ok(attr);
        }

        #region swagger
        /// <summary>
        /// Gets an Attraction item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Attraction
        ///     {
        ///        "id": 1,
        ///        "name": "Attraction",
        ///        "Description": "An Attraction"
        ///     }
        ///
        /// </remarks>
        /// <returns>An Attraction item</returns>
        /// <response code="202">Returns the attraction item</response>
        /// <response code="400">If the attraction item is null</response>
        #endregion
        [HttpGet("{id}", Name = "AttractionById")]
        public ActionResult<List<Attraction>> GetAttraction(int id)
        {
            //if(!_cities.Exists(x => x.Id == cityId) || !_cities.FirstOrDefault(x => x.Id == cityId).attractions.Exists(x => x.Id == attractionId))
            //{
            //    return NotFound();
            //}

            //return new OkObjectResult(_cities.FirstOrDefault(x => x.Id == id).attractions);

            List<Attraction> attr = new List<Attraction>();

            attr = _context.Attractions.Where(x => x.Id == id).ToList();

            if (attr.Count == 0)
            {
                return NotFound();
            }

            return Ok(attr);
        }


        #region swagger
        /// <summary>
        /// Creates an Attraction item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Attraction
        ///     {
        ///        "id": 1,
        ///        "name": "Attraction",
        ///        "Description": An Attraction
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly created Attraction item</returns>
        /// <response code="201">Returns the newly created attraction</response>
        /// <response code="400">If the attraction item is null</response>            
        #endregion
        [HttpPost("Create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateAttraction(Attraction attraction)
        {
            _context.Attractions.Add(attraction);
            _context.SaveChanges();

            return CreatedAtAction("CreateAttraction", attraction);
        }

        #region swagger
        /// <summary>
        /// Edits a specific City item.
        /// </summary>
        #endregion
        [HttpPut("Update")]
        public IActionResult Put(Attraction attraction)
        {
            //for (int i = 0; i < attractions.Count; i++)
            //{
            //    if (attractions[i].Id == id)
            //    {
            //        attractions[i] = attraction;
            //        return Ok();
            //    }
            //}
            //return NotFound();

            var attractionItem = _context.Attractions.Find(attraction.Id);
            if (attractionItem == null)
            {
                return NotFound();
            }

            attractionItem.Description = attraction.Description;
            attractionItem.Name = attraction.Name;

            _context.Attractions.Update(attractionItem);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cityItem = _context.Cities.Find(id);
            if (cityItem == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(cityItem);
            _context.SaveChanges();

            return NoContent();
        }
    }
}