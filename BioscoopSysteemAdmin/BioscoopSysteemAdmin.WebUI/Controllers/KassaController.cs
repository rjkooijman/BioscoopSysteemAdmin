using BioscoopSysteemWebsite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BioscoopSysteemWebsite.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemAdmin.WebUI.Controllers {
    public class KassaController : Controller {
        private IRepository repo;
        private PrintController print;

        public KassaController(IRepository repo) {
            this.repo = repo;
            print = new PrintController(repo);
        }
        public ActionResult Overview(int id)
            //Gemaakt door: Frank Molengraaf
               {
            int dayOffset = id;
            dayOffset = dayOffset > 6 ? 6 : dayOffset;
            DateTime date;
            if (dayOffset == 0) {
                date = DateTime.Now;
            } else {
                date = DateTime.Now.AddDays(dayOffset);
            }
            Dictionary<Movie, List<Show>> listsOfMovies = repo.GetAllShows()
                .Where(r => r.StartTime > date && r.StartTime < date.AddDays(1))
                .GroupBy(r => r.Movie)
                .ToDictionary(group => group.Key, group => group.ToList());

            ViewBag.Userid = 2;
            ViewBag.SelectId = id;
            return View("Overview", listsOfMovies);
        }

        [HttpGet]
        public ViewResult Order(int id, int userid) {
            if (repo.GetUserById(userid).Role.Role == "Kassa") {
                Show show = repo.GetShowByID(id);
                //Get actor

                string[] actorspermovie = new string[show.Movie.Actor.Count];
                int x = 0;
                foreach (Actor actor in show.Movie.Actor) {
                    actorspermovie[x] = actor.ActorName;
                    x++;
                }
                string allActors = string.Join(", <br />", actorspermovie);
                ViewBag.FilmBannerPath = show.Movie.BannerImage;
                ViewBag.Actors = allActors;
                ViewBag.Tickets = getAvailableTickets(show);

                Session["Userid"] = userid;
                //Get shows and other information
                return View(show);
            } else {
                return View("~/Views/Home/Unautorized.cshtml");
            }
        }

        [ExcludeFromCodeCoverage]
        [HttpPost]
        public ViewResult NewOrder() {
            int userid = (int)Session["Userid"];
            if (repo.GetUserById(userid).Role.Role == "Kassa") {
                int id = int.Parse(Request["showId"]);
                if (Request["email"] != "") {
                    char[] splitChar = new Char[] { ',' };
                    string[] types = Request["Type"].Split(splitChar);
                    string[] totals = Request["Amount"].Split(splitChar);
                    string email = Request["email"];
                    for (int i = 0; i < types.Count(); i++) {
                        if (types[i] == "") {
                            types = types.Where(w => w != types[i]).ToArray();
                        }
                    }

                    Show show = repo.GetShowByID(id);
                    ViewBag.error = null;
                    Order order = new Order(show, false);

                    for (int x = 0; x < types.Count(); x++) {

                        TicketSoort t = repo.GetTicketSoort(int.Parse(types[x]));
                        order.AddTicket(t, int.Parse(totals[x]));
                        if (email != null) {
                            order.Email = email;
                        } else {
                            return View("Order", order.ShowID);
                        }
                    }
                    order.UserID = 4;
                    repo.AddOrder(order);
                    Session["OrderID"] = order.OrderId;
                    return View("ManualSeatSelection", order);
                } else {
                    @ViewBag.order = "Vul alle velden in.";
                    return View("Order", id);
                }
            } else {
                return View("~/Views/Home/Unauthorized.cshtml");
            }
        }

        [ExcludeFromCodeCoverage]
        public ViewResult ManualSeatSelection() {
            char[] splitChar = new char[] { ',' };
            int orderId = (int)Session["OrderID"];
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

        [HttpGet]
        public ViewResult Reservation(int userid) {
            if (repo.GetUserById(userid).Role.Role == "Kassa") {
                Session["userid"] = userid;
                return View();
            } else {
                return View("~/Views/Home/Unauthorized.cshtml");
            }
        }

        [HttpPost]
        public ActionResult Reservation(Order order) {
            int userid = (int)Session["userid"];
            if (repo.GetUserById(userid).Role.Role == "Kassa") {
                Order realOrder = repo.GetOrderByID(order.OrderId);
                if (realOrder == null) {
                    ViewBag.Error = "Controleer de reserveringscode, de opgegeven code is niet gevonden";
                    return View();
                } else {
                    ViewBag.Userid = userid;
                    return View("OrderDetails", realOrder);
                }
            } else {
                return View("~/Views/Home/Unauthorized.cshtml");
            }
        }

        public ActionResult ChangeOrder(int id, int userid) {
            if (repo.GetUserById(userid).Role.Role == "Kassa") {
                ViewBag.SeatError = "Deze stoelen zijn nog vrij";
                Order order = repo.GetOrderByID(id);
                Order orderx = new Order(order.Show, false);
                List<Seat> seatList = order.Show.AssignedSeats;
                List<Ticket> ticketList = order.Tickets;
                List<TicketSoort> ticketSoortList = new List<TicketSoort>();
                List<int> aantal = new List<int>();
                List<int> ticketIdList = new List<int>();
                List<int?> seatIdList = new List<int?>();
                if (ticketList.Count() > 0) {
                    foreach (Ticket tick in ticketList) {
                        ticketSoortList.Add(tick.TicketSoort);
                    }
                    for (int i = 0; i < ticketList.Count(); i++) {
                        aantal.Add(ticketSoortList.Where(ts => ts.TicketSoortID == ticketSoortList[i].TicketSoortID).Count());
                    }
                    ticketSoortList = ticketSoortList.Distinct().ToList();

                    for (int i = 0; i < ticketSoortList.Count(); i++) {
                        orderx.AddTicket(ticketSoortList[i], aantal[i]);
                    }

                    for (int x = 0; x < ticketList.Count(); x++) {
                        ticketIdList.Add(ticketList[x].TicketId);
                        seatIdList.Add(ticketList[x].SeatId);
                    }
                    for (int i = 0; i < ticketIdList.Count(); i++) {
                        repo.ChangeTicket(ticketIdList[i], seatIdList[i]);
                    }

                    order.Tickets = orderx.Tickets;
                    Session["OrderID"] = order.OrderId;
                    Session["UserID"] = userid;

                    return View("ManualSeatSelection", order);
                } else {
                    return View("Reservation", order);
                }
            } else {
                return View("~/Views/Home/Unauthorized.cshtml");
            }
        }

        [HttpGet]
        public ViewResult NewSubscriber() {
            return View();
        }

        [HttpPost]
        public ActionResult NewSubscriber(Subscriber subscriber, HttpPostedFileBase image = null) {
            if (ModelState.IsValid) {
                if (image != null) {
                    subscriber.ImageMimeType = image.ContentType;
                    subscriber.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(subscriber.ImageData, 0, image.ContentLength);
                }
                if (repo.GetSubscriberByName(subscriber) == null) {
                    repo.AddSubscriber(subscriber);
                    ViewBag.SubscriberSucces = "Abonnement is aangemaakt.";
                    print.SubscriberPrint(subscriber);
                    return View();
                } else {
                    ViewBag.SubscriberError = "Dit abonnement bestaat al.";
                    return View();
                }


            } else {
                ViewBag.SubscriberError = "Er is iets fout gegaan, probeer het opnieuw.";
                return View();
            }
        }

        [ExcludeFromCodeCoverage]
        private List<TicketSoort> getAvailableTickets(Show show) {
            List<TicketSoort> tickets = repo.GetTicketSoorten().ToList();
            List<TicketSoort> aangebodenTickets = new List<TicketSoort>();

            //Nieuw
            aangebodenTickets.Add(tickets.First(r => r.Naam.Equals("Normaal tickets")));

            if (show.PopcornArrangement) {
                aangebodenTickets.Add(tickets.First(r => r.Naam.Equals("Popcorn arrangement")));
            }
            if (!show.Holiday && (show.StartTime.DayOfWeek > DayOfWeek.Sunday && show.StartTime.DayOfWeek < DayOfWeek.Friday)) {
                aangebodenTickets.Add(tickets.First(r => r.Naam.Equals("Senioren tickets")));
            }
            if (show.StartTime.DayOfWeek > DayOfWeek.Sunday && show.StartTime.DayOfWeek < DayOfWeek.Friday) {
                aangebodenTickets.Add(tickets.First(r => r.Naam.Equals("Student tickets")));

            }
            if (show.Movie.Genre.Equals("Kinderfilm") && show.Movie.Language.Equals("Nederlands") && show.StartTime.Hour < 18) {
                aangebodenTickets.Add(tickets.First(r => r.Naam.Equals("Kinder tickets")));
            }
            if (show.VIP) {
                aangebodenTickets.Add(tickets.First(r => r.Naam.Equals("VIP tickets")));
            }
            return aangebodenTickets;
        }
    }
}