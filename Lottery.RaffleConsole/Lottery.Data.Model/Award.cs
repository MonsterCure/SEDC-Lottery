using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lottery.Data.Model
{
    [Table("Awards")]
    public class Award : IEntity
    {
        [Key]
        [Column("AwardID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AwardName { get; set; }

        public string AwardDescription { get; set; }

        public int AwardQuantity { get; set; }

        public byte RaffledType { get; set; } //ENUM values: Immediate/PerDay/Final
    }
}
