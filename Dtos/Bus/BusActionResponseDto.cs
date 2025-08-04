using System;
using Terminal.Dtos.Common;

namespace Terminal.Dtos.Bus
{
    public class BusActionResponse
    {
        public string Id { get; set; }
        public string NumeroBus { get; set; }
        public string Chofer { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public LocationDto StartLocation { get; set; }
        public LocationDto EndLocation { get; set; }

        // Para Relacion con Companies
        public string CompanyId { get; set; }
    }
}