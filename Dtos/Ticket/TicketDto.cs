using System;

namespace Terminal.API.Dtos.Tickets
{
    public class TicketDto
    {
        public string NumeroTicket { get; set; }
        public int NumeroAsiento { get; set; }
        public decimal ValorBoleto { get; set; }
        public DateTime FechaEmision { get; set; }
        public string SitioSalida { get; set; }
        public DateTime HorarioSalida { get; set; }
        public DateTime HorarioLlegada { get; set; }

        // Relaciones futuras comentadas
        // public string RutaId { get; set; }
        // public string HorarioId { get; set; }
    }
}
