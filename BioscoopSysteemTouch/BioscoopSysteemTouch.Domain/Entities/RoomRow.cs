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
    public class RoomRow
    {
        [Key]
        public int RoomRowID { get; set; }
        public int RowNumber { get; set; }
        public int SeatAmount { get; set; }
        public int RoomId { get; set; }
    }
}
