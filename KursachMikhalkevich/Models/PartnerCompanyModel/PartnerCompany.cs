using Microsoft.AspNetCore.Mvc;
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
        [Remote(action: "CheckName", controller: "PartnerCompany", AdditionalFields = "Id", ErrorMessage = "Данная компания уже существует", HttpMethod = "POST")]
        public string Name { get; set; }

        public virtual List<Practice> Practices { get; set; } = new List<Practice>();
    }
}
