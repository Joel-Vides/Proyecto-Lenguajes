using System;
using Terminal.Dtos.Horarios;

namespace Terminal.Dtos.Ticket
{
    public class TicketDto
    {
        public string Id { get; set; }
        public string NumeroTicket { get; set; }
        public int NumeroAsiento { get; set; }
        public decimal ValorBoleto { get; set; }
        public DateTime FechaEmision { get; set; }

        // Relacion Horario
        public HorarioActionResponseDto Horario { get; set; }
    }
}