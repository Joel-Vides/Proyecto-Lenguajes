using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Terminal.Database.Entities.Common
{
    public class    
        BaseEntity
    {
        [Key]
        [Column("id", TypeName = "nvarchar(100)")]
        public string Id { get; set; }

        [Column("created_by", TypeName = "nvarchar(255)")]
        public string CreatedBy { get; set; }

        [Column("created_date", TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [Column("updated_by", TypeName = "nvarchar(255)")]
        public string UpdatedBy { get; set; }

        [Column("updated_date", TypeName = "datetime2")]
        public DateTime UpdateDate { get; set; }
    }
}
