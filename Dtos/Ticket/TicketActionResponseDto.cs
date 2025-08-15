using Terminal.Dtos.Horarios;

namespace Terminal.Dtos.Ticket
{
    public class TicketActionResponseDto
    {
        public string Id { get; set; }
        public string NumeroTicket { get; set; }
        public int NumeroAsiento { get; set; }
        public decimal ValorBoleto { get; set; }
        public DateTime FechaEmision { get; set; }

        // Relación horario
        public HorarioActionResponseDto Horario { get; set; }
    }
}