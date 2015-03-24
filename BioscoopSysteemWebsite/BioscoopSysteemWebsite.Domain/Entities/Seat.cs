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
    public class Seat
    {
        [Key]
        public int SeatId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public int ShowID { get; set; }

    }
}
