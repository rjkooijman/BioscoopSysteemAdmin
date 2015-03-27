using BioscoopSysteemWebsite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BioscoopSysteemWebsite.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemAdmin.WebUI.Controllers
{
    public class KassaController : Controller
    {
        private IRepository repo;
        private PrintController print;

        public KassaController(IRepository repo) {
            this.repo = repo;
            print = new PrintController(repo);
        }

        public ActionResult Overview(int userid) {
            if (repo.GetUserById(userid).Role.Role == "Kassa") {
                Dictionary<int, List<Show>> showsAtRooms = new Dictionary<int, List<Show>>();
                List<Show> toekomstigeShows = repo.GetAllShows().ToList().Where(r => r.StartTime > DateTime.Now && r.StartTime < DateTime.Now.AddDays(1)).ToList();
                List<Show> showOverview = new List<Show>();
                ViewBag.Userid = userid;

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

                    return View("Overview", showOverview);
                } else {
                    return View("Empty");
                }
            } else {
                return View("~/Views/Home/Unauthorized.cshtml");
            }
        }

        [HttpGet]
        public ActionResult Order(int id, bool secretFilm, int userid) {
            if (repo.GetUserById(userid).Role.Role == "Kassa") {
                Show show = repo.GetShowByID(id);
                ViewBag.Userid = userid;

                if (secretFilm) {
                    show.Movie.Name = "Geheime film";
                    show.Movie.ImageData = "geencover.jpg";
                    show.Movie.Description = "";
                    ViewBag.secretFilm = true;
                } else {
                    ViewBag.secretFilm = false;
                }

                ViewBag.Tickets = getAvailableTickets(show);
                return View(show);
            } else {
                return View("~/Views/Home/Unautorized.cshtml");
            }
        }

        [HttpPost]
        public ActionResult Order(bool secretFilm, int userid) {
            if (repo.GetUserById(userid).Role.Role == "Kassa") {
                Order order;
                
                char[] splitChar = new Char[] { ',' };
                string[] types = Request["Type"].Split(splitChar);
                string[] totals = Request["Amount"].Split(splitChar);
                int[] intTotals = new int[totals.Count()];
                int id = int.Parse(Request["showId"]);
                string autoAssign = Request["autoAssign"];

                bool geenKaartBesteld = true;

                int z = 0;
                foreach (string totalRow in totals) {
                    int parsedInt;
                    if (int.TryParse(totalRow, out parsedInt) && parsedInt > 0) {
                        intTotals[z] = parsedInt;
                        geenKaartBesteld = false;
                    }
                    z++;
                }
                if (geenKaartBesteld == true) {
                    return View("Order", repo.GetShowByID(id));
                }

                for (int i = 0; i < types.Count(); i++) {
                    if (types[i] == "") {
                        types = types.Where(t => t != types[i]).ToArray();
                    }
                }

                Show show = repo.GetShowByID(id);
                order = new Order(show, secretFilm);
                order.Email = "ticketmachine@avanscinema.nl";
                order.User = repo.GetUserById(userid);


                for (int i = 0; i < types.Count(); i++) {
                    TicketSoort t = repo.GetTicketSoort(int.Parse(types[i]));
                    order.AddTicket(t, int.Parse(totals[i]));
                }

                if (autoAssign == null) {
                    order.AssignAutoSeats();
                    repo.AddOrder(order);
                    return View("Payment", order);
                } else {
                    repo.AddOrder(order);
                    Session["OrderID"] = order.OrderId;
                    Session["UserID"] = userid;
                    return View("ManualSeatSelection", order);
                }
            } else {
                return View("~/Views/Home/Unauthorized.cshtml");
            }
        }

        [ExcludeFromCodeCoverage]
        public ViewResult ManualSeatSelection() {
            char[] splitChar = new char[] { ',' };
            int orderId = (int)Session["OrderID"];
            int userid = (int)Session["UserID"];
            Order order = repo.GetOrderByID(orderId);

            string[] seatData = Request["seat"].Split(splitChar);
            string[] splittedSeatString;
            splitChar = new char[] { '_' };
            int[] rowNumbers = new int[seatData.Count()];
            int[] seatNumbers = new int[seatData.Count()];

            int x = 0;
            foreach (string seatString in seatData) {
                splittedSeatString = seatString.Split(splitChar);
                rowNumbers[x] = int.Parse(splittedSeatString[0]);
                seatNumbers[x] = int.Parse(splittedSeatString[1]);

                x++;
            }

            order.AssignManualSeats(rowNumbers, seatNumbers);
            repo.UpdateOrder(order);
            ViewBag.Userid = userid;

            return View("Payment", order);
        }

        public ViewResult Pay(Order order, int userid) {
            if (repo.GetUserById(userid).Role.Role == "Kassa") {
                //Call the update order method
                int orderid = int.Parse(order.OrderId.ToString());
                Order orderx = repo.GetOrderByID(orderid);

                repo.UpdateOrder(orderx);
                print.CreateObject(orderx);

                return View("Paid", orderx);
            } else {
                return View("~/Views/Home/Unauthorized.cshtml");
            }

        }

        private List<TicketSoort> getAvailableTickets(Show show) {
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
                if (show.VIP) {
                    aangebodenTickets.Add(tickets.First(r => r.Naam.Equals("VIP tickets")));
                }
            return aangebodenTickets;
        }
    }
}