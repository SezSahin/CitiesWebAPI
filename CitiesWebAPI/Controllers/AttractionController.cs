using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class AttractionController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly DataContext _context;

        public AttractionController(DataContext context, UnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
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
            return new OkObjectResult(_unitOfWork.Attraction.GetAll());
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

            var attraction = _context.Attractions.Where(x => x.Id == id).ToList();

            if (attraction.Count == 0)
            {
                return NotFound();
            }

            return new OkObjectResult(_unitOfWork.Attraction.Get(id));
        }

        [HttpGet]
        [Route("attractions/{id}")]
        public IActionResult GetAttractionsByCity(int id)
        {
            var cities = _context.Cities.ToList();

            if (_unitOfWork.City.Get(id) is null)
            {
                return NotFound();
            }

            return new OkObjectResult(_unitOfWork.Attraction.GetAttractionsByCity(id));
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
            _unitOfWork.Attraction.Add(attraction);
            try
            {
                _unitOfWork.Complete();
            }
            catch (Exception)
            {
                return Conflict();
            }

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

            Attraction attractionItem = _unitOfWork.Attraction.Get(attraction.Id);
            if (attractionItem is null)
            {
                return NotFound();
            }

            attractionItem.Description = attraction.Description;
            attractionItem.Name = attraction.Name;

            try
            {
                _unitOfWork.Complete();
            }
            catch (Exception)
            {
                return Conflict();
            }

            return NoContent();
        }

        [HttpPatch]
        [Route("Update/{id}")]
        public IActionResult Patch(JsonPatchDocument<Attraction> attractionPatch, int id)
        {
            var attractionItem = _unitOfWork.Attraction.Get(id);
            if (attractionItem is null)
            {
                return NotFound();
            }
            try
            {
                attractionPatch.ApplyTo(attractionItem);
                _unitOfWork.Complete();

                return Ok();
            }
            catch (Exception)
            {
                return Conflict();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var attractionItem = _unitOfWork.Attraction.Get(id);
            if (attractionItem is null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.Attraction.Remove(attractionItem);
                _unitOfWork.Complete();

                return NoContent();
            }
            catch (Exception)
            {
                return Conflict();
            }
        }
    }
}