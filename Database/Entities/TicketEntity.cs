using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Terminal.Database.Entities.Common;

namespace Terminal.Database.Entities
{
    [Table("tickets")]
    public class TicketEntity : BaseEntity
    {
        [Column("numero_ticket")]
        public string NumeroTicket { get; set; }

        [Column("numero_asiento")]
        [Required]
        public int NumeroAsiento { get; set; }

        [Column("fecha_emision")]
        [Required]
        public DateTime FechaEmision { get; set; }

        // Relación con Horario
        [Column("horario_id")]
        public int HorarioId { get; set; }
        public virtual HorarioEntity Horario { get; set; }
    }
}