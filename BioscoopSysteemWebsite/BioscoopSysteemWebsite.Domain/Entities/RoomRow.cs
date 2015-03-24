using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemWebsite.Domain.Entities
{
    //Gemaakt door Gregor Hoogstad
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
