using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Models
{
    [Table("subjects")]
    public class Subject
    {
        [Column("subject_id")]
        public int Id { get; set; }

        [Column("subject_name")]
        public string Name { get; set; }

        [Column("worker_id")]
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }



        public virtual List<SubjectGroup> SubjectsGroups { get; set; } = new List<SubjectGroup>();
        public virtual List<Group> Groups { get; set; } = new List<Group>();

    }
}
