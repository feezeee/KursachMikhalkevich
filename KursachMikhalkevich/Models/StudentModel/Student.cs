using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Models
{
    [Table("students")]
    public class Student
    {
        [Column("student_id")]
        public int Id { get; set; }

        [Column("student_first_name")]
        public string FirstName { get; set; }

        [Column("student_last_name")]
        public string LastName { get; set; }

        [Column("student_middle_name")]
        public string MiddleName { get; set; }


        [Column("graduate_work")]
        public string GraduateWork { get; set; }



        [Column("practic_id")]
        public int? PracticeId { get; set; }
        public Practice Practice { get; set; }
    }
}
