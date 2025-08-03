using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Persons.API.Database.Entities.Common;
using Terminal.Database.Entities.Common;

namespace Terminal.Database.Entities
{
    [Table("bus")]
    public class BusEntity : BaseEntity
    {
        [Column("numero_bus")]
        [Required]
        public string NumeroBus { get; set; }

        [Column("chofer")]
        [Required]
        public string Chofer { get; set; }

        [Column("modelo")]
        [Required]
        public string Modelo { get; set; }

        [Column("anio")]
        [Required]
        public int Anio { get; set; }

        [Required]
        [Column("start_latitude")]
        public double StartLatitude { get; set; }

        [Required]
        [Column("start_longitude")]
        public double StartLongitude { get; set; }

        [Required]
        [Column("end_latitude")]
        public double EndLatitude { get; set; }

        [Required]
        [Column("end_longitude")]
        public double EndLongitude { get; set; }
    }
}