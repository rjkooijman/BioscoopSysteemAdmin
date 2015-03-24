using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BioscoopSysteemWebsite.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemWebsite.Domain.Concrete
{
    [ExcludeFromCodeCoverage]
    public class EFDbContext : DbContext
    {
        //Gemaakt door: Frank Molengraaf

        public DbSet<Order> Orders { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Pegi> Pegis { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketSoort> TicketSoort { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Mail> Emails { get; set; }
        public DbSet<LadiesNight> LadiesNights { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
    }
}
