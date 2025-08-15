using Terminal.Dtos.Ticket;

namespace Terminal.Dtos.Horarios
{
    public class HorarioActionResponseDto
    {
        public int Id { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraLlegada { get; set; }
        public decimal Precio { get; set; }

        // Relaciones
        public int RutaId { get; set; }
        public string NombreRuta { get; set; }
        public string CiudadOrigen { get; set; }
        public string CiudadDestino { get; set; }

        public string BusId { get; set; }
        public string NumeroBus { get; set; }
        public string ModeloBus { get; set; }

        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<TicketDto> Tickets { get; set; }
    }
}