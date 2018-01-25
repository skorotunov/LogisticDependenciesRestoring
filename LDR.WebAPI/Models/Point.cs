using System.ComponentModel.DataAnnotations;

namespace LDR.WebAPI.Models
{
    public class Point
    {
        [Key]
        public long Id { get; set; }

        public double X { get; set; }

        public double Y { get; set; }
    }
}
