using BioscoopSysteemWebsite.Domain.Entities;
using BioscoopSysteemWebsite.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Threading;
using System;
using BioscoopSysteemTouch.WebUI.Controllers;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemTouch.WebUI.Controllers {
    public class HomeController : Controller {
        private IRepository repo;
        private PrintController print;


        public HomeController(IRepository repo) {
            this.repo = repo;
            print = new PrintController(repo);
        }
        
        public ActionResult Index() {
            //Gemaakt door: Frank Molengraaf
            return View();
        }

        public ActionResult Overview() {
            //Gemaakt door: Frank Molengraaf & Gregor Hoogstad
            string view;
            Dictionary<int, List<Show>> showsAtRooms = new Dictionary<int, List<Show>>();
            List<Show> toekomstigeShows = repo.GetAllShows().ToList().Where(r => r.StartTime > DateTime.Now && r.StartTime < DateTime.Now.AddDays(1)).ToList();
            List<Show> showOverview = new List<Show>();

            if (toekomstigeShows != null && toekomstigeShows.Count > 0) {
                foreach (Room room in repo.GetRooms().ToList()) {

                    Show show = toekomstigeShows.Where(r => r.Room.RoomId == room.RoomId).OrderBy(r => r.Movie.StartDate).ToList().FirstOrDefault();
                    if (show != null) {
                        showOverview.Add(show);
                    }
                }
                showOverview = showOverview.OrderBy(r => r.Movie.Name).ToList();

                Show geheimeShow = showOverview.OrderByDescending(r => (r.Room.GetCapacity() - r.AssignedSeats.Count)).First();
                showOverview.Add(geheimeShow);

                view = "Overview";
                return View(view, showOverview);
            } else {
                view = "Empty";
                return View(view);
            }

        }

        //Geschreven door Gregor Hoogstad
        [HttpGet]
        public ViewResult Order(int id, bool secretShow)
        {
            Show show = repo.GetShowByID(id);

            if (secretShow)
            {
                show.Movie.Name = "Verrassing film";
                show.Movie.ImageData = "geencover.jpg";
                show.Movie.Description = "";
                ViewBag.SecretShow = true;
            }
            else
            {
                ViewBag.SecretShow = false;
            }
            ViewBag.Tickets = getAvailableTickets(show);
            return View(show);
        }

        //Geschreven door Gregor Hoogstad
        [ExcludeFromCodeCoverage]
        [HttpPost]
        public ViewResult NewOrder(bool secretShow) {
            Order order;
            char[] splitChar = new Char[] { ',' };
            string[] types = Request["Type"].Split(splitChar);
            string[] totals = Request["Amount"].Split(splitChar);
            int[] intTotals = new int[totals.Count()];
            int id = int.Parse(Request["showId"]);
            string autoAssign = Request["autoAssign"];

            bool geenKaartenBestelt = true;

             int z = 0;
            foreach (string totalRow in totals)
            {
                int parsedInt;
                if(int.TryParse(totalRow, out parsedInt) && parsedInt > 0){
                    intTotals[z] = parsedInt;
                    geenKaartenBestelt = false;
                }
                z++;
            }

            if (geenKaartenBestelt == true)
            {
                return View("Order", repo.GetShowByID(id));
            }

            for(int i = 0; i < types.Count(); i++) {
                if (types[i] == "") {
                    types = types.Where(w => w != types[i]).ToArray();
                }
            }

            Show show = repo.GetShowByID(id);
            order = new Order(show, secretShow);
            order.Email = "ticketmachine@machine.nl";


            for (int x = 0; x < types.Count(); x++) {
                TicketSoort t = repo.GetTicketSoort(int.Parse(types[x]));
                order.AddTicket(t, int.Parse(totals[x]));
            }

            if (autoAssign == null)
            {
                order.AssignAutoSeats();
                repo.AddOrder(order);
                return View("Payment", order);
            }
            else
            {
                repo.AddOrder(order);
                ViewBag.OrderId = order.OrderId;
                return View("ManualSeatSelection", order);
            }

        }

        //Gemaakt door: Ricardo Jobse
        [HttpGet]
        public ViewResult Reservation() {
            return View();
        }

        //Gemaakt door: Ricardo Jobse
        [ExcludeFromCodeCoverage]
        [HttpPost]
        public ViewResult Reservation(Order order) {
            if (order.OrderId != 0) { 
                order = repo.GetOrderByID(order.OrderId);
                if (order != null)
                {
                    if (order.PickedUp == true)
                    {
                        return View("PickedUp", order);
                    }
                    else if (order.Paid != true)
                    {
                        return View("Payment", order);
                    }
                    else
                    {
                        return Pay(order);
                    }
                }
                else
                {
                    //Reservation id is not known
                    ViewBag.Unknown = "Er is geen reservering gevonden met dit nummer.";
                    return View();
                }
            } 
            else 
            {
                // there is a validation error
                ViewBag.Unknown = "Er is een ongeldige code ingevoerd.";
                return View();
            }
        }

        //Gemaakt door: Ricardo Jobse
        public ViewResult Pay(Order order) {
            //Call the update order method
            int orderid = int.Parse(order.OrderId.ToString());
            Order orderx = repo.GetOrderByID(orderid);

            repo.UpdateOrder(orderx);
            print.CreateObject(orderx);

            return View("Paid", orderx);

        }

        [ExcludeFromCodeCoverage]
        public ActionResult RegisterManualSelection(int orderId)
        {
            char[] splitChar = new char[] { ',' };
            Order order = repo.GetOrderByID(orderId);

            string[] seatData = Request["seat"].Split(splitChar);
            string[] splittedSeatString;
            splitChar = new char[] { '_' };
            int[] rowNumbers = new int[seatData.Count()];
            int[] seatNumbers = new int[seatData.Count()];

            int x = 0;
            foreach (string seatString in seatData)
            {
                splittedSeatString = seatString.Split(splitChar);
                rowNumbers[x] = int.Parse(splittedSeatString[0]);
                seatNumbers[x] = int.Parse(splittedSeatString[1]);

                x++;
            }

            order.AssignManualSeats(rowNumbers, seatNumbers);
            repo.UpdateOrder(order);

            return View("Payment", order);
        }
        [ExcludeFromCodeCoverage]
        private List<TicketSoort> getAvailableTickets(Show show)
        {
            List<TicketSoort> tickets = repo.GetTicketSoorten().ToList();
            List<TicketSoort> aangebodenTickets = new List<TicketSoort>();

            //Nieuw
                aangebodenTickets.Add(tickets.First(r => r.Naam.Equals("Normaal tickets")));

                if (show.PopcornArrangement)
                {
                    aangebodenTickets.Add(tickets.First(r => r.Naam.Equals("Popcorn arrangement")));
                }
                if (!show.Holiday && (show.StartTime.DayOfWeek > DayOfWeek.Sunday && show.StartTime.DayOfWeek < DayOfWeek.Friday))
                {
                    aangebodenTickets.Add(tickets.First(r => r.Naam.Equals("Senioren tickets")));
                }
                if (show.StartTime.DayOfWeek > DayOfWeek.Sunday && show.StartTime.DayOfWeek < DayOfWeek.Friday)
                {
                    aangebodenTickets.Add(tickets.First(r => r.Naam.Equals("Student tickets")));

                }
                if (show.Movie.Genre.Equals("Kinderfilm") && show.Movie.Language.Equals("Nederlands") && show.StartTime.Hour < 18)
                {
                    aangebodenTickets.Add(tickets.First(r => r.Naam.Equals("Kinder tickets")));
                }
            return aangebodenTickets;
        }
    }
}
