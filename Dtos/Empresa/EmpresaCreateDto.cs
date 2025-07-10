using System.ComponentModel.DataAnnotations;

namespace Terminal.Dtos.Empresa
{
    public class EmpresaCreateDto
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El {0} debe tener entre {2} y {1} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")] // Validación de formato email
        public string Email { get; set; }

        [Display(Name = "Número de contacto")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [RegularExpression(@"^\+?[\d\s-]{10,15}$", ErrorMessage = "Número inválido")] // Valida formato telefónico
        public string PhoneNumber { get; set; }
    }
}
