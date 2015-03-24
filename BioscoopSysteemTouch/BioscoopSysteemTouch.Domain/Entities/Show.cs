using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioscoopSysteemTouch.Domain.Entities {
    public class Show {

        [Key]
        public int ShowID { get; set; }
        public DateTime StartTime { get; set; }
        public bool PopcornArrangement { get; set; }
        public virtual Movie Movie { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public virtual List<Seat> AssignedSeats { get; set; }

        public List<Seat> AssignSeats(int seatAmount) 
        {
            //Create a grid of the row layout of a room.
            int[][] roomGrid = Room.createRoomGrid();

            Debug.WriteLine("__ROOMGRID__");
            showVisualDebugGrid(roomGrid);

            //Fill the grid with all assinged seats.
            int[][] filledRoomGrid = FillRoomGrid(AssignedSeats, roomGrid);

            Debug.WriteLine("__FILLEDROOMGRID__");
            showVisualDebugGrid(roomGrid);

            //Assign the new seats a place in the room.
            List<Seat> newSeats = AddSeatsInRoom(roomGrid, seatAmount);

            if (newSeats == null)
            {
                newSeats = new List<Seat>();

                //For each seat try find a spot.
                for (int x = 0; x < seatAmount; x++)
                {
                    List<Seat> tempList = AddSeatsInRoom(roomGrid, 1);
                    //If there isn't any space left at all return null
                    if (tempList == null)
                return null;
                    newSeats.AddRange(tempList);
                }
            }
                //Register the seats as taken for the show.
                AssignedSeats.AddRange(newSeats);

            Debug.WriteLine("__CHANGEDROOMGRID__");
            showVisualDebugGrid(roomGrid);

                //Return the seats so they can be attached to the tickets
                return newSeats;

        }

        public int[][] getFilledRoomGrid()
        {
            return FillRoomGrid(AssignedSeats, Room.createRoomGrid());
        }

        private int[][] FillRoomGrid(List<Seat> occupiedSeats, int[][] roomGrid) {
            //Add each occupied seat to the grid.
            foreach (Seat seat in occupiedSeats) {
                //Reduce the number with one sice arrays work on a zero based index.
                roomGrid[seat.Row - 1][seat.Number - 1] = seat.SeatId;
            }
            return roomGrid;
        }

        private List<Seat> AddSeatsInRoom(int[][] roomGrid, int numberOfSeats) {
            //Specify vars
            int offset, initialPosition;
            List<Seat> seats = null;

            for (int x = roomGrid.GetLength(0) - 1; x > -1; x--) {
                //Determine starting position (middle of the row in this case)
                initialPosition = (int)(roomGrid[x].GetLength(0) / 2) - 1;
                //Reset the offset number
                offset = 0;
                //Reset the searching direction
                int direction = 0;

                for (int y = 0; y < roomGrid[x].GetLength(0); y++) {
                    //Determine in which direction we are searching and manipulate the positions accordingly. 
                    switch (direction) {
                        //Left
                        case 0:
                            seats = CheckPosition(roomGrid[x], initialPosition - offset, numberOfSeats, x);
                            direction++;
                            break;
                        //Right
                        case 1:
                            offset++;
                            seats = CheckPosition(roomGrid[x], initialPosition + offset, numberOfSeats, x);
                            direction--;
                            break;
                    }

                    //If checkposition detected space and assigned the seats cancel the loop and return the filled in seats.
                    if (seats != null) {
                        return seats;
                    }
                }
            }
            //If there wasn't any space for the seats return null.
            return null;
        }

        private List<Seat> CheckPosition(int[] roomRowGrid, int position, int amount, int rowNumber) {
            //If the position is beyond the boundaries of the row return null.
            if (position < 0 || position > roomRowGrid.GetLength(0) - 1) {
                return null;
            } else {
                //Assign vars
                int begin;
                bool space = true;

                //Determine the begin position for the check.
                begin = position - ((amount - 1) / 2);

                //Determine if the boundaries off the position including offsets exceed the row.
                if (begin < 0 || begin + amount > roomRowGrid.Count()) {
                    return null;
                }

                //Check if all the positions between start en end are free, if a single space isn't free cancel the search immediantly.
                for (int x = begin; x < begin + amount; x++) {
                    if (roomRowGrid[x] != 0) {
                        space = false;
                        break;
                    }
                }

                //If all the spaces were free.
                if (space == true) {
                    //Create a new list of Seat objects and assign them the correct row and seat number
                    List<Seat> seats = new List<Seat>();
                    for (int x = begin; x < begin + amount; x++) {
                        seats.Add(new Seat() {
                            Row = rowNumber + 1,
                            Number = x + 1
                        });
                    }
                    //Return the seats list if the search was succesfull.
                    return seats;
                } else {
                    //Return null if the search wasn't succesfull
                    return null;
                }
            }
        }

        //Debug method for visual debugging.
        private void showVisualDebugGrid(int[][] grid)
        {
            for (int firstDimensionLength = 0; firstDimensionLength < grid.GetLength(0); firstDimensionLength++)
            {
                for (int secondDimensionLength = 0; secondDimensionLength < grid[firstDimensionLength].GetLength(0); secondDimensionLength++)
                {
                    Debug.Write(getIndicationChar(grid[firstDimensionLength][secondDimensionLength]) + " ");
                }
                Debug.WriteLine(' ');
            }
        }

        private Char getIndicationChar(int charID)
        {
            switch (charID)
            {
                case 0: return '_';
                case 1: return 'Y';
                default: return 'x';
            }
        }

        public List<Seat> AssignManualSeats(int[] rowNumbers, int[] seatNumbers)
        {
            List<Seat> seats = new List<Seat>();
            for (int x = 0; x < rowNumbers.Count(); x++)
            {
                seats.Add(new Seat() { Row = rowNumbers[x] + 1, Number = seatNumbers[x] + 1 });
            }
            AssignedSeats.AddRange(seats);
            return seats;
        }
    }
}