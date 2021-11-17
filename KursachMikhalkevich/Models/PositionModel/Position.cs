using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [Remote(action: "CheckName", controller: "Position", AdditionalFields = "Id", ErrorMessage = "Данная должность уже существует", HttpMethod = "POST")]

        public string Name { get; set; }

        public virtual List<Worker> Workers { get; set; } = new List<Worker>();
    }
}
