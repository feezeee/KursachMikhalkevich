using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Models
{
    [Table("practices")]
    public class Practice
    {
        [Column("practic_id")]
        public int Id { get; set; }

        [Column("practit_name")]
        public string Name { get; set; }

        [Column("practic_address")]
        public string Address { get; set; }


        [Column("partner_company_id")]
        public int PartnerCompanyId { get; set; }
        public PartnerCompany PartnerCompany { get; set; }



        public virtual List<Student> Students { get; set; } = new List<Student>();
    }
}
