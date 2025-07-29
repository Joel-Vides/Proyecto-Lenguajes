namespace Terminal.Dtos.Horarios
{
    public class HorarioDto
    {
        public int Id { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraLlegada { get; set; }
        public decimal Precio { get; set; }
        //public int RutaId { get; set; }

    }
}
