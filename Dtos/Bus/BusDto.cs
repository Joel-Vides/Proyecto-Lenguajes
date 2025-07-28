namespace Terminal.API.DTOs
{
    public class BusDto
    {
        public Guid Id { get; set; }
        public string NumeroBus { get; set; }
        public string Chofer { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
    }
}
