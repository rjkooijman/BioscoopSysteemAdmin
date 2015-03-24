using BioscoopSysteemTouch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioscoopSysteemTouch.Domain.Interfaces {
    public interface IFilmRepository {
        IEnumerable<Movie> GetAll();

        Movie GetByID(int id);

        IEnumerable<Order> Orders { get; }

        IEnumerable<Ticket> Tickets { get; }

        IEnumerable<Show> GetAllShows();

        IEnumerable<Room> GetRooms();

        bool UpdateOrder(Order order);

        TicketSoort GetTicketSoort(int id);

        Show GetShowByID(int id);

        void AddTicket(Ticket ticket);

        void AddOrder(Order order);

        Order GetOrderByID(int orderid);
    }
}
