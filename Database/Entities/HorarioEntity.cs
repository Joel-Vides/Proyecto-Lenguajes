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
        public DateTime HoraSalida { get; set; }

        [Column("hora_llegada")]
        [Required]
        public DateTime HoraLlegada { get; set; }

        [Column("precio")]
        [Required]
        public decimal Precio { get; set; }

        // Relación de muchos a uno con Ruta
        [Column("ruta_id")]
        public int RutaId { get; set; }
        public virtual RutaEntity Ruta { get; set; }

        //Relación de muchos a uno con Bus
        [Column("bus_id")]
        public string BusId { get; set; }
        public virtual BusEntity Bus { get; set; }

        //Relación de muchos a uno con Company
        [Column("company_id")]
        public string CompanyId { get; set; }
        public virtual CompanyEntity Company { get; set; }

        //Relación de uno a muchos con Ticket
        public virtual ICollection<TicketEntity> Tickets { get; set; } = new List<TicketEntity>();
    }
}
