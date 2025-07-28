using System.ComponentModel.DataAnnotations;

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
        public int Año { get; set; }

        [Display(Name = "Operadora")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public int OperadoraId { get; set; }
    }
}
