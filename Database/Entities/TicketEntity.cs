using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Terminal.Database.Entities.Common;

namespace Terminal.API.Database.Entities
{
    [Table("tickets")]
    public class TicketEntity : BaseEntity
    {
        [Column("numero_ticket")]
        [Required]
        public string NumeroTicket { get; set; }

        [Column("numero_asiento")]
        [Required]
        public int NumeroAsiento { get; set; }

        [Column("valor_boleto")]
        [Required]
        public decimal ValorBoleto { get; set; }

        [Column("fecha_emision")]
        [Required]
        public DateTime FechaEmision { get; set; }

        //[Column("sitio_salida")]
        //[Required]
        //public string SitioSalida { get; set; }

        //[Column("horario_salida")]
        //[Required]
        //public DateTime HorarioSalida { get; set; }

        //[Column("horario_llegada")]
        //[Required]
        //public DateTime HorarioLlegada { get; set; }

        // Relación futura: public string HorarioId { get; set; }
        // [ForeignKey(nameof(HorarioId))]
        // public virtual HorarioEntity Horario { get; set; }

        // Relación futura: public string RutaId { get; set; }
        // [ForeignKey(nameof(RutaId))]
        // public virtual RutaEntity Ruta { get; set; }
    }
}
