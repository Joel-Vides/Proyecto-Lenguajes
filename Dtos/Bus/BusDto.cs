using Terminal.Dtos.Common;

namespace Terminal.API.DTOs
{
    public class BusDto
    {
        public string Id { get; set; }
        public string NumeroBus { get; set; }
        public string Chofer { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public LocationDto StartLocation { get; set; }
        public LocationDto EndLocation { get; set; }

        public string? ImageUrl { get; set; }

        // Empresas
        public string CompanyId { get; set; }
    }
}