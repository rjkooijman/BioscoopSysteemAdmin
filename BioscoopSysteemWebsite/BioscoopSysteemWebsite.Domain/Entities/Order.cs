using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioscoopSysteemWebsite.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public bool Paid { get; set; }
        public bool PickedUp { get; set; }

        [Required(ErrorMessage = "Gelieve uw email in te vullen")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "gelieve een geldig email adres in te vullen")]
        public string Email { get; set; }

        public virtual Show Show { get; set; }
        public int ShowID { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
        public bool SecretShowOrder { get; set; }

        public Order()
        {
            SecretShowOrder = false;
        }

        public Order(Show showParam, bool secretShow)
        {
            SecretShowOrder = secretShow;
            Show = showParam;
            Tickets = new List<Ticket>();
        }

        //Geschreven door Robert-Jan Kooijman
        public void AddTicket(TicketSoort ticketsoort, int totaal)
        {
            for (int x = 0; x < totaal; x++)
            {

                Tickets.Add(new Ticket() { TicketSoort = ticketsoort });
            }
        }

        //Geschreven door Gregor Hoogstad
        public void AssignAutoSeats()
        {
            List<Seat> seats = Show.AssignSeats(Tickets.Count());

            var ticketZip = Tickets.Zip(seats, (l, r) => new { Ticket = l, Seat = r });

            foreach (var ticketSeat in ticketZip)
            {
                ticketSeat.Ticket.Seat = ticketSeat.Seat;
            }
        }

        //Geschreven door Gregor Hoogstad
        [ExcludeFromCodeCoverage]
        public void AssignManualSeats(int[] rowNumbers, int[] seatNumbers)
        {
            List<Seat> seats = Show.AssignManualSeats(rowNumbers, seatNumbers);
            for (int x = 0; x < Tickets.Count(); x++)
            {
                Tickets[x].Seat = seats[x];
            }
        }

        //Geschreven door Robert-Jan Kooijman & Gregor Hoogstad
        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            decimal ticketPrice;
            foreach (Ticket ticket in Tickets)
            {
                ticketPrice = ticket.TicketSoort.Price;

                //Check if movie is a 3D movie
                if (Show.Movie.Type)
                {
                    ticketPrice += 2.50m;
                }
                //Check movie is longer then 120 minutes
                if (Show.Movie.Duration > 120)
                {
                    ticketPrice += 0.50m;
                }
                //Check if show is a secret show
                if (SecretShowOrder)
                {
                    ticketPrice -= 2.00m;
                }

                totalPrice += ticketPrice;
            }
            return totalPrice;
        }
    }
}
