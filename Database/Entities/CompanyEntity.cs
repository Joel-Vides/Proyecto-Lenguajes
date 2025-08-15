using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Terminal.Database.Entities.Common;

namespace Terminal.Database.Entities
{
    [Table("companies")]
    public class CompanyEntity : BaseEntity
    {
        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone_number")]
        [Required]
        public string PhoneNumber { get; set; }

        [Column("image_url")]
        public string? ImageUrl { get; set; }

        // Relación uno a muchos con Bus
        public virtual ICollection<BusEntity> Buses { get; set; } = new List<BusEntity>();

        //Relación con Horario
        public virtual ICollection<HorarioEntity> Horarios { get; set; } = new List<HorarioEntity>();

        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}