using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LDR.WebAPI.Models
{
    public class Point
    {
        public long Id { get; set; }

        [Required]
        public double X { get; set; }

        [DefaultValue(2.1)]
        public double Y { get; set; }
    }
}
