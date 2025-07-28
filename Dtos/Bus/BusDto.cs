namespace Terminal.Dtos.Bus
{
    public class BusDto
    {
        public int Id { get; set; }
        public string NumeroBus { get; set; }
        public string Chofer { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        public int OperadoraId { get; set; }

    }
}
