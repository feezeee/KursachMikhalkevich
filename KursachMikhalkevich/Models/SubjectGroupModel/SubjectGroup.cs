using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Models
{
    [Table("subjects_has_groups")]
    public class SubjectGroup
    {
        [Column("subject_id")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        [Column("group_id")]
        public int GroupId { get; set; }
        public Group Group { get; set; }

        [Column("date")]
        public DateTime DateTime { get; set; }
    }
}
