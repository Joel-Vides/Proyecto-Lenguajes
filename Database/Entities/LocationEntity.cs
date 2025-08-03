using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Terminal.Database.Entities.Common
{
    [ComplexType]
    public class LocationEntity
    {
        [Column("latitude")]
        [Required]
        public double Latitude { get; set; }

        [Column("longitude")]
        [Required]
        public double Longitude { get; set; }
    }
}