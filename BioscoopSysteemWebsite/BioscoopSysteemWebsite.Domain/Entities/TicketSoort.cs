using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemWebsite.Domain.Entities
{
    //Geschreven door Gregor Hoogstad
    [ExcludeFromCodeCoverage]
    public class TicketSoort
    {
        [Key]
        public int TicketSoortID { get; set; }
        public string Naam { get; set; }
        public string EnglishTicketName { get; set; }
        public decimal Price { get; set; }
    }
}
