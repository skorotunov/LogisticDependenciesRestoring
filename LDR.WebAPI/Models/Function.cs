using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LDR.WebAPI.Models
{
    public class Function
    {
        public Function(int count)
        {
            this.PointsCount = count;
            this.Points = new Dictionary<int, KeyValuePair<double, double>>(count);
        }

        public int PointsCount { get; set; }

        public Dictionary<int, KeyValuePair<double, double>> Points { get; set; }
    }
}
