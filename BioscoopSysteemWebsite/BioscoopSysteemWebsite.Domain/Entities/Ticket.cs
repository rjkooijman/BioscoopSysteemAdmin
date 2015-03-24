using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemWebsite.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public virtual TicketSoort TicketSoort { get; set; }
        public virtual Seat Seat { get; set; }
        public int OrderId { get; set; }
        public int? SeatId { get; set; }
        public int TicketSoortID { get; set; }
    }
}
