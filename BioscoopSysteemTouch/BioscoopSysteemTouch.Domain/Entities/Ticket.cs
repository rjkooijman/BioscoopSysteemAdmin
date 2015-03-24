using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemTouch.Domain.Entities {
    [ExcludeFromCodeCoverage]
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public int TicketSoortID { get; set; }
        public virtual TicketSoort TicketSoort { get; set; }
        public int? SeatId { get; set; }
        public virtual Seat Seat { get; set; }
        public int OrderId { get; set; }


        public decimal getPrice()
        {
            return TicketSoort.Price;
        }
    }
}
