using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Models
{
    [Table("workers")]
    public class Worker
    {
        [Column("worker_id")]
        
        public int Id { get; set; }

        [Column("worker_first_name")]
        [Required]
        public  string FirstName { get; set; }

        [Column("worker_last_name")]
        [Required]
        public string LastName { get; set; }

        [Column("worker_middle_name")]
        [Required]
        public string MiddleName { get; set; }

        [Column("password")]
        [Required]
        public string Password { get; set; }

        [Column("email")]
        [Required]
        [Remote(action: "CheckEmail", controller: "Worker", AdditionalFields = "Id", ErrorMessage = "Данная почта указана для другого сотрудника", HttpMethod = "POST")]
        public string Email { get; set; }


        [Column("position_id")]
        public int PositionId { get; set; }
        public Position Position { get; set; }



        [Column("access_right_id")]
        public int AccessRightId { get; set; }
        public AccessRight AccessRight { get; set; }



        [Column("qualification_id")]
        public int QualificationId { get; set; }
        public Qualification Qualification { get; set; }

        public virtual List<Subject> Subjects { get; set; } = new List<Subject>();
        public virtual List<Student> Students { get; set; } = new List<Student>();

    }
}
