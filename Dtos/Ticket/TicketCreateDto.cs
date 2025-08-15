using System.ComponentModel.DataAnnotations;

namespace Terminal.Dtos.Ticket
{
    public class TicketCreateDto
    {
        [Display(Name = "Número de Asiento")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, 100, ErrorMessage = "El {0} debe estar entre {1} y {2}.")]
        public int NumeroAsiento { get; set; }

        [Display(Name = "Valor del Boleto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, 2000, ErrorMessage = "El {0} debe ser mayor a cero.")]
        public decimal ValorBoleto { get; set; }

        [Display(Name = "ID de Horario")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int HorarioId { get; set; }
    }
}