using LDR.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
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
    }
}
