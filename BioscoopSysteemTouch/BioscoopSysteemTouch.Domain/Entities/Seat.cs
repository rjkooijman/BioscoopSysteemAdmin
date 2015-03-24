using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioscoopSysteemTouch.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Seat
    {
        [Key]
        public int SeatId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public int ShowID { get; set; }
    }
}
