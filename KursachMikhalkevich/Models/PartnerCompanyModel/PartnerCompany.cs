using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Models
{
    [Table("partner_companies")]
    public class PartnerCompany
    {
        [Column("partner_company_id")]
        public int Id { get; set; }

        [Column("company_name")]
        public string Name { get; set; }

        public virtual List<Practice> Practices { get; set; } = new List<Practice>();
    }
}
