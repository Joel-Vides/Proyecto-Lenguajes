using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Terminal.Database.Entities.Common;

namespace Terminal.Database.Entities
{
    [Table("ruta")]
    public class RutaEntity : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string SitioSalida { get; set; }

        [Required]
        [MaxLength(100)]
        public string SitioDestino { get; set; }

        [Required]
        public string TotalKilometros { get; set; }

        // Ignoramos relaciones por ahora
        public int BusId { get; set; }
        public int CompanyId { get; set; }
    }
}
