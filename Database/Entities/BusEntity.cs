using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Persons.API.Database.Entities.Common;

namespace Terminal.Database.Entities
{
    [Table("bus")]
    public class BusActionResponseDto : BaseEntity
    {

        [Column("numero_bus")]
        [Required]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone_number")]
        [Required]
        public string PhoneNumber { get; set; }

    }
}