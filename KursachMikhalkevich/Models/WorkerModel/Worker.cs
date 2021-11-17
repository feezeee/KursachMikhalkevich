using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public  string FirstName { get; set; }

        [Column("worker_last_name")]
        public string LastName { get; set; }

        [Column("worker_middle_name")]
        public string MiddleName { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("email")]
        [Remote(action: "CheckEmail", controller: "Worker", AdditionalFields = "ClassId", ErrorMessage = "В это врямя занятие уже проводится", HttpMethod = "POST")]
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

    }
}
