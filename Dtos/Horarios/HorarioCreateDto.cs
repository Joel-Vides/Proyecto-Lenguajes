namespace Terminal.Dtos.Horarios
{
    public class HorarioCreateDto
    {
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraLlegada { get; set; }
        public decimal Precio { get; set; }
        public int RutaId { get; set; }

    }
}
