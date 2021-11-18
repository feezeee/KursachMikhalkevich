using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [Remote(action: "CheckName", controller: "Group", AdditionalFields = "Id", ErrorMessage = "Данная группа уже существует", HttpMethod = "POST")]
        public string Name { get; set; }

        public virtual List<SubjectGroup> SubjectsGroups { get; set; } = new List<SubjectGroup>();
        public virtual List<Subject> Subjects { get; set; } = new List<Subject>();
        public virtual List<Student> Students { get; set; } = new List<Student>();

    }
}
