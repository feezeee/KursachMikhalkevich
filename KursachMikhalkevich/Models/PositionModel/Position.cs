using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Models
{
    [Table("positions")]
    public class Position
    {
        [Column("position_id")]
        public int Id { get; set; }

        [Column("position_name")]
        public string Name { get; set; }

        public virtual List<Worker> Workers { get; set; } = new List<Worker>();
    }
}
