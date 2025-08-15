namespace Terminal.Dtos.Horarios
{
    public class HorarioCreateDto
    {
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraLlegada { get; set; }
        public decimal Precio { get; set; }
        
        //Claves foraneas
        public int RutaId { get; set; }
        public string BusId { get; set; }
        public string CompanyId { get; set; }
    }
}