using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemTouch.Domain.Entities {
    [ExcludeFromCodeCoverage]
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public bool Accessibility { get; set; }
        public bool Type { get; set; }

        public virtual List<RoomRow> Rows { get; set; }

        public int[][] createRoomGrid()
        {
            if(Rows == null || Rows.Count == 0)
            {
                throw new Exception("This room hasn't got any rows specified.");
            }

            int[][] roomGrid = new int[Rows.Count][];
            foreach(RoomRow row in Rows)
            {
                roomGrid[(row.RowNumber - 1)] = new int[row.SeatAmount];
            }

            return roomGrid;
        }

        public int GetCapacity()
        {
            int cap = 0;
            foreach (RoomRow row in Rows)
            {
                cap += row.SeatAmount;
            }

            return cap;
        }
    }
}
