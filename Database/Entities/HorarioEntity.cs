using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Terminal.Database.Entities.Common;

namespace Terminal.Database.Entities
{
    [Table("horarios")]
    public class HorarioEntity : BaseEntity
    {
        [Column("hora_salida")]
        [Required]
        public TimeSpan HoraSalida { get; set; }

        [Column("hora_llegada")]
        [Required]
        public TimeSpan HoraLlegada { get; set; }

        [Column("precio")]
        [Required]
        public decimal Precio { get; set; }

        [Column("ruta_id")]
        [Required]
        public int RutaId { get; set; }
    }
}
