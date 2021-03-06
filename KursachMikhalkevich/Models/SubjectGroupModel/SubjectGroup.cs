using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Models
{
    [Table("timetables")]
    public class SubjectGroup
    {
        [Column("timetable_id")]
        [Required]
        public int Id { get; set; }

        [Column("subject_id")]
        [Required]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        [Column("group_id")]
        [Required]
        public int GroupId { get; set; }
        public Group Group { get; set; }

        [Column("date_time_start")]
        public DateTime DateTimeStart { get; set; }

        [Column("date_time_end")]
        public DateTime DateTimeEnd { get; set; }
    }
}
