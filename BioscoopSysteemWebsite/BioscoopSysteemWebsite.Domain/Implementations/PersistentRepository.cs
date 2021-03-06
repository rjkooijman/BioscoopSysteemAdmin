﻿using BioscoopSysteemWebsite.Domain.Concrete;
using BioscoopSysteemWebsite.Domain.Entities;
using BioscoopSysteemWebsite.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Data.Entity;

namespace BioscoopSysteemWebsite.Domain.Implementations
{
    [ExcludeFromCodeCoverage]
    public class PersistentRepository : IRepository
    {
        private EFDbContext context = new EFDbContext();

        [ExcludeFromCodeCoverage]
        public IEnumerable<Show> GetAllShows()
        {
            return context.Shows;
        }
        public void ChangeSeats(int seatNumber, int rowNumber) {
            foreach (Seat seat in context.Seats) {
                if (seat.Number == seatNumber && seat.Row == rowNumber) {
                    context.Seats.Remove(seat);
                }
            }
            context.SaveChanges();
        }

        public void ChangeTicket(int? ticketid, int? seatid) {
            foreach (Seat seat in context.Seats) {
                if (seat.SeatId == seatid) {
                    context.Seats.Remove(seat);
                }
            }
            foreach (Ticket ticket in context.Tickets) {
                if (ticket.TicketId == ticketid) {
                    context.Tickets.Remove(ticket);
                }
            }
            
            context.SaveChanges();
        }

        public Movie GetByID(int id)
        {
            //Gemaakt door: Frank Molengraaf
            return context.Movies.Where(x => x.MovieId == id).FirstOrDefault();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            //Gemaakt door: Frank Molengraaf
            return context.Movies;
        }

        public Show GetShowByID(int id)
        {
            return context.Shows.Where(x => x.ShowID == id).FirstOrDefault();
        }

        public void Order(Order order) {
            Order changedorder = context.Orders.Where(o => o.OrderId == order.OrderId).First();
            changedorder = order;
            context.SaveChanges();
        }
        //Gemaakt door: Ricardo Jobse
        public IEnumerable<Order> Orders
        {
            get
            {
                return context.Orders;
            }
        }

        public IEnumerable<Ticket> Tickets
        {
            get
            {
                return context.Tickets;
            }
        }

        public IEnumerable<Room> GetRooms()
        {
            return context.Rooms;
        }

        public IEnumerable<Ticket> GetTickets()
        {
            return context.Tickets;
        }

        public TicketSoort GetTicketSoort(int ticketSoortID)
        {
            return context.TicketSoort.Where(r => r.TicketSoortID == ticketSoortID).First();
        }

        //Gemaakt door: Ricardo Jobse & Gregor Hoogstad
        public bool UpdateOrder(Order order)
        {
            Order changedOrder = order;
            bool result;

            if (changedOrder.Paid.Equals(false))
            {
                //Payment will be done.
                //Order will be picked up.
                Order dbEntry = context.Orders.Find(changedOrder.OrderId);
                if (dbEntry != null)
                {
                    dbEntry.Paid = true;
                    dbEntry.PickedUp = true;
                }
                result = true;
            }
            else
            {
                result = false;
            }

            context.SaveChanges();
            return result;
        }

        public void AddTicket(Ticket ticket)
        {
            context.Tickets.Add(ticket);
        }

        public void AddOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        //Gemaakt door: Ricardo Jobse
        public IEnumerable<Order> GetAllOrders()
        {
            return context.Orders;
        }

        //Gemaakt door: Ricardo Jobse
        public Order GetOrderByID(int orderid)
        {
            return context.Orders.Where(x => x.OrderId == orderid).FirstOrDefault();
        }

        public bool AddMailForNewsletter(Mail mail)
        {
            bool b = false;
            if (mail != null)
            {
                context.Emails.Add(mail);
                b = true;
            }
            context.SaveChanges();

            return b;
        }

        public IEnumerable<Mail> GetAllMails()
        {
            return context.Emails;
        }

        [ExcludeFromCodeCoverage]
        public List<TicketSoort> GetTicketSoorten()
        {
            return context.TicketSoort.ToList();
        }

        [ExcludeFromCodeCoverage]
        public User GetUserById(int id) {
            return context.Users.Where(x => x.UserId == id).FirstOrDefault();
        }

        [ExcludeFromCodeCoverage]
        public IEnumerable<Room> GetAllRooms() {
            return context.Rooms;
        }

        [ExcludeFromCodeCoverage]
        public IEnumerable<LadiesNight> GetAllLadiesNights() {
            return context.LadiesNights;
        }

        [ExcludeFromCodeCoverage]
        public void AddShow(Show show) {
            context.Shows.Add(show);
            context.SaveChanges();
        }

        [ExcludeFromCodeCoverage]
        public Movie GetMovieById(int id) {
            return context.Movies.Where(m => m.MovieId == id).First();
        }

        [ExcludeFromCodeCoverage]
        public Room GetRoomById(int id) {
            return context.Rooms.Where(r => r.RoomId == id).First();
        }

        [ExcludeFromCodeCoverage]
        public LadiesNight GetLadiesNightByDate(DateTime date) {
            return context.LadiesNights.Where(ln => ln.LadiesNightDay == date).FirstOrDefault();
        }

        [ExcludeFromCodeCoverage]
        public void AddTicketSoort(TicketSoort ticket) {
            context.TicketSoort.Add(ticket);
            context.SaveChanges();
        }

        public void AddSubscriber(Subscriber subscriber) {
            context.Subscribers.Add(subscriber);
            context.SaveChanges();
        }

        public bool DuplicateSubscriber(Subscriber subscriber) {
            bool b = false;
            foreach(Subscriber sub in context.Subscribers) {
                if(sub == subscriber){
                    b = true;
                } else {
                    b = false;
                }
            }
            return b;
        }

        public List<Subscriber> GetAllSubscribers() {
            return context.Subscribers.ToList();
        }
    }
}
