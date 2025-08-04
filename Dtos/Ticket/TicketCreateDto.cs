using System.ComponentModel.DataAnnotations;

namespace Terminal.API.Dtos.Tickets
{
    public class TicketCreateDto
    {
        [Display(Name = "Número de Ticket")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
        public string NumeroTicket { get; set; }

        [Display(Name = "Número de Asiento")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, 100, ErrorMessage = "El {0} debe estar entre {1} y {2}.")]
        public int NumeroAsiento { get; set; }

        [Display(Name = "Valor del Boleto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, 2000, ErrorMessage = "El {0} debe ser mayor a cero.")]
        public decimal ValorBoleto { get; set; }

        [Display(Name = "Fecha de Emisión")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FechaEmision { get; set; }

        [Display(Name = "Sitio de Salida")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
        public string SitioSalida { get; set; }

        [Display(Name = "Horario de Salida")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime HorarioSalida { get; set; }

        [Display(Name = "Horario de Llegada")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime HorarioLlegada { get; set; }

        // Futuras relaciones: HorarioId, RutaId, etc.
    }
}
