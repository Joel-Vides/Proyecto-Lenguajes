using System;

namespace Terminal.Dtos.Bus
{
    public class BusActionResponse
    {
        public Guid Id { get; set; }

        public string NumeroBus { get; set; }

        public string Chofer { get; set; }

        public string Modelo { get; set; }

        public int Anio { get; set; }

    }
}
