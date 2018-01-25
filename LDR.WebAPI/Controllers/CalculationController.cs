using LDR.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LDR.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CalculationController : ControllerBase
    {
        private readonly LDRContext context;

        public CalculationController(LDRContext context)
        {
            this.context = context;
            if (this.context.Points.Count() == 0)
            {
                this.context.Points.Add(new Point { X = 2.1, Y = 3.21 });
                this.context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns the list of all points.
        /// </summary>
        /// <returns>The list of all points</returns>
        /// <response code="200">If everyting is worked like expected</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<Point> GetAll()
        {
            return this.context.Points;
        }

        /// <summary>
        /// Returns point by it's id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Point object</returns>
        /// <response code="200">Returns the point</response>
        /// <response code="404">If the point was not founded</response>
        [HttpGet("{id}", Name = "GetPoint")]
        [ProducesResponseType(typeof(Point), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(long id)
        {
            Point point = this.context.Points.SingleOrDefault(x => x.Id == id);
            if (point == null)
            {
                return this.NotFound();
            }

            return new ObjectResult(point);
        }

        /// <summary>
        /// Creates a point.
        /// </summary>
        /// <param name="point"></param>
        /// <returns>A newly-created point</returns>
        /// <response code="201">Returns the newly-created point</response>
        /// <response code="400">If the point is null or if there any points with the same id</response>
        [HttpPost]
        [ProducesResponseType(typeof(Point), 201)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] Point point)
        {
            if (point == null || this.context.Points.Any(x => x.Id == point.Id))
            {
                return this.BadRequest();
            }

            this.context.Points.Add(point);
            this.context.SaveChanges();
            return this.CreatedAtRoute("GetPoint", new { id = point.Id }, point);
        }

        /// <summary>
        /// Updates a point.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="point"></param>
        /// <returns>No content result</returns>
        /// <response code="204">Returns nothing if everyting is worked like expected</response>
        /// <response code="400">If the point is null or if the id of the point is not equal to id as a parameter</response>
        /// <response code="404">If it was not founded point with such id for updating</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(long id, [FromBody] Point point)
        {
            if (point == null || point.Id != id)
            {
                return this.BadRequest();
            }

            Point item = this.context.Points.SingleOrDefault(x => x.Id == id);
            if (item == null)
            {
                return this.NotFound();
            }

            item.X = point.X;
            item.Y = point.Y;
            this.context.Points.Update(item);
            this.context.SaveChanges();
            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific point.
        /// </summary>
        /// <param name="id"></param>   
        /// <response code="204">Returns nothing if everyting is worked like expected</response>
        /// <response code="404">If it was not founded point with such id for deleting</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(long id)
        {
            Point point = this.context.Points.SingleOrDefault(x => x.Id == id);
            if (point == null)
            {
                return this.NotFound();
            }

            this.context.Points.Remove(point);
            this.context.SaveChanges();
            return new NoContentResult();
        }
    }
}
