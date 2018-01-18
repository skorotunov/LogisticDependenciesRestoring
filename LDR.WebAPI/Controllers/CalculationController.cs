using LDR.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LDR.WebAPI.Controllers
{
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
            var item = this.context.Points.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return this.NotFound();
            }

            return new ObjectResult(item);
        }
    }
}
