using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Models
{
    [Table("groups")]
    public class Group
    {
        [Column("group_id")]
        public int Id { get; set; }

        [Column("group_name")]
        public string Name { get; set; }

        public virtual List<SubjectGroup> SubjectsGroups { get; set; } = new List<SubjectGroup>();
        public virtual List<Subject> Subjects { get; set; } = new List<Subject>();
        public virtual List<Student> Students { get; set; } = new List<Student>();

    }
}
