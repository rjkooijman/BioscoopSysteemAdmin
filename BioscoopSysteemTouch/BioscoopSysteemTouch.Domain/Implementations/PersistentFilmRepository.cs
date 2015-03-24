using BioscoopSysteemTouch.Domain.Concrete;
using BioscoopSysteemTouch.Domain.Entities;
using BioscoopSysteemTouch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioscoopSysteemTouch.Domain.Implementations {
    public class PersistentFilmRepository : IFilmRepository {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Movie> GetAll() {
            return context.Movies;
        }

        public IEnumerable<Show> GetAllShows()
        {
            return context.Shows;
        }

        public Movie GetByID(int id) {
            return context.Movies.Where(x => x.MovieId == id).FirstOrDefault();
        }

        public Show GetShowByID(int id) {
            return context.Shows.Where(x => x.ShowID == id).FirstOrDefault();
        }

        public IEnumerable<Order> Orders {
            get {
                return context.Orders;
            }
        }

        public IEnumerable<Ticket> Tickets {
            get {
                return context.Tickets;
            }
        }
        public IEnumerable<Room> GetRooms()
        {
            return context.Rooms;
        }

        public IEnumerable<Ticket> GetTickets() {
            return context.Tickets;
        }

        public TicketSoort GetTicketSoort(int ticketSoortID)
        {
            return context.TicketSoort.Where(r => r.TicketSoortID == ticketSoortID).First();
        }

        public bool UpdateOrder(Order order) {
            Order changedOrder = order;

            if (changedOrder.Paid.Equals(false)) {
                //Payment will be done.
                //Order will be picked up.
                Order dbEntry = context.Orders.Find(changedOrder.OrderId);
                if (dbEntry != null) {
                    dbEntry.Paid = true;
                    dbEntry.PickedUp = true;
                    context.SaveChanges();
                }
                return true;

            } else {
                return false;
            }
        }

        public void AddTicket(Ticket ticket) {
            context.Tickets.Add(ticket);
        }

        public void AddOrder(Order order) {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public Order GetOrderByID(int orderid) {
            return context.Orders.Where(x => x.OrderId == orderid).FirstOrDefault();
        }

    }
}
