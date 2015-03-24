using BioscoopSysteemWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioscoopSysteemWebsite.Domain.Interfaces {
    public interface IRepository {

        //Geschreven door Frank Molengraaf
        Movie GetByID(int id);

        //Geschreven door Robert-Jan Kooijman
        IEnumerable<Order> Orders { get; }

        //Geschreven door Robert-Jan Kooijman
        IEnumerable<Ticket> Tickets { get; }

        //Geschreven door Frank Molengraaf
        IEnumerable<Show> GetAllShows();

        //Geschreven door Gregor Hoogstad
        IEnumerable<Room> GetRooms();

        //Geschreven door Robert-Jan Kooijman
        IEnumerable<Order> GetAllOrders();

        //Geschreven door Ricardo Jobse
        bool UpdateOrder(Order order);

        //Geschreven door Robert-Jan Kooijman
        TicketSoort GetTicketSoort(int id);

        //Geschreven door Robert-Jan Kooijman
        Show GetShowByID(int id);

        //Geschreven door Robert-Jan Kooijman
        void AddTicket(Ticket ticket);

        //Geschreven door Gregor Hoogstad
        void AddOrder(Order order);

        //Geschreven door Gregor Hoogstad
        Order GetOrderByID(int orderid);
        
        //Geschreven door Frank Molengraaf
        IEnumerable<Movie> GetAllMovies();

        //Geschreven door Robert-Jan Kooijman
        bool AddMailForNewsletter(Mail mail);

        //Geschreven door Robert-Jan Kooijman
        IEnumerable<Mail> GetAllMails();

        IEnumerable<TicketSoort> GetTicketSoorten();

        User GetUserById(int id);

        void AddMovie(Movie movie, Pegi pegi);
    }
}
