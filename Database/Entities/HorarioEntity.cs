using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Terminal.Database.Entities.Common;

namespace Terminal.Database.Entities
{
    [Table("horarios")]
    public class HorarioEntity : BaseEntity
    {
        [Column("hora_llegada")]
        [Required]
        [MaxLength(100)]
        public string HoraSalida { get; set; }

        [Column("hora_salida")]
        [Required]
        [MaxLength(100)]
        public string HoraLlegada { get; set; }

        [Column("precio")]
        [Required]
        public string Precio { get; set; }

        //[Column("bus_id")]
        //[Required]
        //public int BusId { get; set; }

        //[Column("company_id")]
        //[Required]
        //public int CompanyId { get; set; }
    }
}
