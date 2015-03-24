using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioscoopSysteemTouch.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class TicketSoort
    {
        [Key]
        public int TicketSoortID { get; set; }
        public string Naam { get; set; }
        public decimal Price { get; set; }
    }
}
