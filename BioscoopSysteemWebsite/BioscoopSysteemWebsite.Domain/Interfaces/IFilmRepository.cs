using BioscoopSysteemWebsite.Domain.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemWebsite.Domain.Interfaces
{
    
    public interface IFilmRepository {
        [ExcludeFromCodeCoverage]
        IEnumerable<Movie> GetAll();
        [ExcludeFromCodeCoverage]
        Movie GetByID(int id);
        [ExcludeFromCodeCoverage]
        IEnumerable<Order> Orders { get; }
        [ExcludeFromCodeCoverage]
        IEnumerable<Ticket> Tickets { get; }
        [ExcludeFromCodeCoverage]
        IEnumerable<Show> GetAllShows();
        [ExcludeFromCodeCoverage]
        IEnumerable<Room> GetRooms();
        [ExcludeFromCodeCoverage]
        bool UpdateOrder(Order order);
        [ExcludeFromCodeCoverage]
        TicketSoort GetTicketSoort(int id);
        [ExcludeFromCodeCoverage]
        Show GetShowByID(int id);
        [ExcludeFromCodeCoverage]
        void AddTicket(Ticket ticket);
        [ExcludeFromCodeCoverage]
        void AddOrder(Order order);
        [ExcludeFromCodeCoverage]
        Order GetOrderByID(int orderid); 
    }
}
