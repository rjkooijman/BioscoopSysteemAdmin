using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioscoopSysteemTouch.Domain.Entities {
    public class Order {

        [Required(ErrorMessage = "Gelieve een reserveringscode in te vullen")]
        [Key]
        public int OrderId { get; set; }

        public bool Paid { get; set; }
        public bool PickedUp { get; set; }
        public int ShowID { get; set; }
        public virtual Show Show { get; set; }
        public virtual List<Ticket> Tickets { get; set; }

        public Order() {
           // Tickets.Add(new Ticket() { TicketSoort = new TicketSoort() { Price = 11, Naam = "iets" }, Seat = new Seat() { Number = 20, Row = 4 } });
        }

        public Order(Show showParam) {
            Show = showParam;
            Tickets = new List<Ticket>();
        }

        public void AddTicket(TicketSoort ticketsoort, int totaal) {
            for (int x = 0; x < totaal; x++) {

                Tickets.Add(new Ticket() { TicketSoort = ticketsoort});
            }
        }

        public void AssignAutoSeats()
        {
            List<Seat> seats = Show.AssignSeats(Tickets.Count());

            var ticketZip = Tickets.Zip(seats, (l, r) => new { Ticket = l, Seat = r });

            foreach (var ticketSeat in ticketZip)
            {
                ticketSeat.Ticket.Seat = ticketSeat.Seat;
            }
        }

        public void AssignManualSeats(int[] rowNumbers, int[] seatNumbers)
        {
            List<Seat> seats = Show.AssignManualSeats(rowNumbers, seatNumbers);
            for(int x = 0; x < Tickets.Count(); x++)
            {
                Tickets[x].Seat = seats[x];
            }
        }

        public decimal GetTotalPrice() {
            decimal tprice = 0;
            foreach (Ticket ticket in Tickets) {
                if (Show.PopcornArrangement) {
                    if (Show.Movie.Duration > 120) {
                        tprice += ticket.TicketSoort.Price + decimal.Parse("3,50");
                    } else {
                        tprice += ticket.TicketSoort.Price + decimal.Parse("3,00");
                    }
                } else if (Show.Movie.Duration > 120) {
                    tprice += ticket.TicketSoort.Price + decimal.Parse("0,50");
                } else {
                    tprice += ticket.TicketSoort.Price;
                }
                /*tprice += Show.Movie.Duration > 120 ? ticket.TicketSoort.Price + decimal.Parse("0.50") : ticket.TicketSoort.Price;*/
            }
            return tprice;
        }
    }
}