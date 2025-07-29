namespace Terminal.Dtos.Horarios
{
    public class HorarioActionResponseDto
    {
        public int Id { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraLlegada { get; set; }
        public decimal Precio { get; set; }

        //public int RutaId { get; set; }

    }
}
