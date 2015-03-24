using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BioscoopSysteemTouch.Domain.Entities;

namespace BioscoopSysteemTouch.Domain.Concrete
{
    public class EFDbContext : DbContext {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Pegi> Pegis { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketSoort> TicketSoort { get; set; }
    }
}
