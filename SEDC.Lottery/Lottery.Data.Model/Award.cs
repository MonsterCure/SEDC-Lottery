using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Data.Model
{
    [Table("dbo.Awards")]
    public class Award : IEntity
    {
        [Key]
        [Column("AwardID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AwardName { get; set; }

        public string AwardDescription { get; set; }

        public int AwardQuantity { get; set; }

        public byte RuffledType { get; set; } //ENUM values: Immediate/PerDay/Final
    }
}
