using LDR.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LDR.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
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

        [HttpGet]
        public IEnumerable<Point> GetAll()
        {
            return this.context.Points;
        }

        [HttpGet("{id}", Name = "GetPoint")]
        public IActionResult GetById(long id)
        {
            Point point = this.context.Points.FirstOrDefault(x => x.Id == id);
            if (point == null)
            {
                return this.NotFound();
            }

            return new ObjectResult(point);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Point), 201)]
        [ProducesResponseType(typeof(Point), 400)]
        public IActionResult Create([FromBody] Point point)
        {
            if (point == null)
            {
                return this.BadRequest();
            }

            this.context.Points.Add(point);
            this.context.SaveChanges();

            return this.CreatedAtRoute("GetPoint", new { id = point.Id }, point);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Point point)
        {
            if (point == null || point.Id != id)
            {
                return this.BadRequest();
            }

            Point item = null;
            try
            {
                item = this.context.Points.SingleOrDefault(x => x.Id == id);
            }
            catch (InvalidOperationException)
            {
                return this.StatusCode(500);
            }

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

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Point point = null;
            try
            {
                point = this.context.Points.SingleOrDefault(x => x.Id == id);
            }
            catch (InvalidOperationException)
            {
                return this.StatusCode(500);
            }

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
