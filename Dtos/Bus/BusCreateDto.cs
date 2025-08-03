using System.ComponentModel.DataAnnotations;
using Terminal.Dtos.Common;

namespace Terminal.Dtos.Bus
{
    public class BusCreateDto
    {
        [Display(Name = "Número de Bus")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(20, ErrorMessage = "El {0} no puede tener más de {1} caracteres")]
        public string NumeroBus { get; set; }

        [Display(Name = "Chofer")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(100, ErrorMessage = "El {0} no puede tener más de {1} caracteres")]
        public string Chofer { get; set; }

        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(50, ErrorMessage = "El {0} no puede tener más de {1} caracteres")]
        public string Modelo { get; set; }

        [Display(Name = "Año")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [Range(1990, 2100, ErrorMessage = "El {0} debe estar entre {1} y {2}")]
        public int Anio { get; set; }

        // Para lo Ubicacion en el Mapa
        [Display(Name = "Ubicación de Inicio")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public LocationDto StartLocation { get; set; }

        [Display(Name = "Ubicación de Destino")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public LocationDto EndLocation { get; set; }
    }
}