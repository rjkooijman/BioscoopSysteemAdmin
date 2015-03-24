using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BioscoopSysteemWebsite.Domain.Entities;
using BioscoopSysteemWebsite.Domain.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Threading;

namespace BioscoopSysteemWebsite.WebUI.Controllers
{
    public class HomeController : Controller
    {

        private IRepository repo;
        private PrintController print;
        private List<SelectListItem> playDay;
        private List<SelectListItem> times;

        public HomeController(IRepository repo)
        {
            this.repo = repo;

            times = new List<SelectListItem>();

            times.Add(new SelectListItem { Text = "12:00", Value = "12" });
            times.Add(new SelectListItem { Text = "13:00", Value = "13" });
            times.Add(new SelectListItem { Text = "14:00", Value = "14" });
            times.Add(new SelectListItem { Text = "15:00", Value = "15" });
            times.Add(new SelectListItem { Text = "16:00", Value = "16" });
            times.Add(new SelectListItem { Text = "17:00", Value = "17" });
            times.Add(new SelectListItem { Text = "18:00", Value = "18" });
            times.Add(new SelectListItem { Text = "19:00", Value = "19" });
            times.Add(new SelectListItem { Text = "20:00", Value = "20" });
            times.Add(new SelectListItem { Text = "21:00", Value = "21" });
            times.Add(new SelectListItem { Text = "22:00", Value = "22" });
            times.Add(new SelectListItem { Text = "23:00", Value = "23" });
            times.Add(new SelectListItem { Text = "", Value = null, Selected = true });
            setDays(false);

            ViewBag.SearchTime = times;

            print = new PrintController(repo);
        }
        //Geschreven door Frank Molengraaf
        [ExcludeFromCodeCoverage]
        [HttpGet]
        public ActionResult Index()
        //Gemaakt door: Frank Molengraaf
        {
            if (Session["Language"] == null)
            {
                Session["Language"] = false;
            }
            List<Show> model = repo.GetAllShows().Where(x => x.StartTime > DateTime.Now).ToList();

            return View(model);
        }

        //Geschreven door Frank Molengraaf & Ricardo Jobse
        [ExcludeFromCodeCoverage]
        [HttpPost]
        public ActionResult Index(string searchString, string playDay, string searchTime)
        {
            IEnumerable<Show> movies = repo.GetAllShows().Where(r => r.StartTime > DateTime.Now && r.StartTime <= DateTime.Now.AddDays(7));
            List<Show> movieList = movies.ToList();

            searchString = searchString.ToLower();
            playDay = playDay.ToLower();

            movieList.Clear();

            //Zoeken op Dag
            if (!string.IsNullOrEmpty(playDay))
            {
                movies = movies.Where(x => x.StartTime.DayOfWeek.ToString().ToLower().Contains(playDay));
            }

            //Zoeken op Titel
            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Movie.Name.ToLower().Contains(searchString));
            }

            //Zoeken op Tijd
            if (!string.IsNullOrEmpty(searchTime))
            {
                movies = movies.Where(t => t.StartTime.TimeOfDay.Hours.ToString().ToLower().Contains(searchTime));
            }


            foreach (Show s in movies)
            {
                movieList.Add(s);
            }

            if (movieList.Count() == 0)
            {
                if (Session["Language"].Equals(false))
                {
                    ViewBag.Result = "Er zijn geen zoekresultaten!";
                }
                else
                {
                    ViewBag.Result = "There are no searchresults!";
                }
            }
            else
            {
                if (Session["Language"].Equals(false))
                {
                    ViewBag.Result = "Uw zoekresultaten:";
                }
                else
                {
                    ViewBag.Result = "Your searchresults:";
                }
            }


            return View("SearchResult", movieList.GroupBy(r => r.Movie).ToDictionary(g => g.Key, g => g.ToList()));
        }

        //Geschreven door Gregor Hoogstad & Frank Molengraaf
        public ActionResult Overview(int id)
        //Gemaakt door: Frank Molengraaf
        {
            int dayOffset = id;
            dayOffset = dayOffset > 6 ? 6 : dayOffset;
            DateTime date;
            if (dayOffset == 0)
            {
                date = DateTime.Now;
            }
            else
            {
                date = DateTime.Now.AddDays(dayOffset);
            }
            Dictionary<Movie, List<Show>> listsOfMovies = repo.GetAllShows()
                .Where(r => r.StartTime > date && r.StartTime < date.AddDays(1))
                .GroupBy(r => r.Movie)
                .ToDictionary(group => group.Key, group => group.ToList());

            ViewBag.SelectId = id;
            return View("Overview", listsOfMovies);
        }

        //Geschreven door Robert-Jan Kooijman
        [ExcludeFromCodeCoverage]
        [HttpGet]
        public ViewResult Order(int id)
        {
            Show show = repo.GetShowByID(id);
            //Get actor

            string[] actorspermovie = new string[show.Movie.Actor.Count];
            int x = 0;
            foreach (Actor actor in show.Movie.Actor)
            {
                actorspermovie[x] = actor.ActorName;
                x++;
            }
            string allActors = string.Join(", <br />", actorspermovie);
            ViewBag.Actors = allActors;
            ViewBag.Tickets = getAvailableTickets(show);

            //Get shows and other information
            return View(show);
        }

        //Geschreven door Gregor Hoogstad & Robert-Jan Kooijman
        [ExcludeFromCodeCoverage]
        [HttpPost]
        public ViewResult NewOrder()
        {

            int id = int.Parse(Request["showId"]);
            if (Request["email"] != "")
            {
                char[] splitChar = new Char[] { ',' };
                string[] types = Request["Type"].Split(splitChar);
                string[] totals = Request["Amount"].Split(splitChar);
                string email = Request["email"];
                for (int i = 0; i < types.Count(); i++)
                {
                    if (types[i] == "")
                    {
                        types = types.Where(w => w != types[i]).ToArray();
                    }
                }

                Show show = repo.GetShowByID(id);
                ViewBag.error = null;
                Order order = new Order(show, false);

                for (int x = 0; x < types.Count(); x++)
                {

                    TicketSoort t = repo.GetTicketSoort(int.Parse(types[x]));
                    order.AddTicket(t, int.Parse(totals[x]));
                    if (email != null)
                    {
                        order.Email = email;
                    }
                    else
                    {
                        return View("Order", order.ShowID);
                    }
                }
                repo.AddOrder(order);
                Session["OrderID"] = order.OrderId;
                return View("ManualSeat", order);
            }
            else
            {
                if (Session["Language"].Equals(false))
                {
                    @ViewBag.order = "Vul alle velden in.";
                }
                else
                {
                    @ViewBag.order = "Please fill in all fields.";
                }
                return View("Order", new List<Show> { repo.GetShowByID(id) });
            }
        }

        //Geschreven door Robert-Jan Kooijman
        [ExcludeFromCodeCoverage]
        [HttpPost]
        public ActionResult Newsletter()
        {
            char[] splitchar = new Char[] { ',' };
            string email = Request["emailadres"];
            IEnumerable<Mail> maillist = repo.GetAllMails();
            List<Show> model = repo.GetAllShows().ToList();
            Mail newMail = new Mail();

            foreach (Mail mail in maillist)
            {
                if (!mail.checkMailAdres(email))
                {
                    if (Session["Language"].Equals(false))
                    {
                        @ViewBag.email = "U bent al ingeschreven voor de nieuwsbrief.";
                    }
                    else
                    {
                        @ViewBag.email = "You have already been subscribed to the newsletter.";
                    }
                    return View("Index", model);
                }
                else
                {
                    if (Session["Language"].Equals(false))
                    {
                        ViewBag.email = "U bent ingeschreven voor de nieuwsbrief.";
                    }
                    else
                    {
                        ViewBag.email = "You are subscribed to the newsletter.";
                    }
                    newMail.MailAdres = email;
                }
            }
            repo.AddMailForNewsletter(newMail);
            newMail.SendEmail();
            return View("Index", model);
        }

        //Geschreven door Ricardo Jobse
        public ActionResult Search(String searchBy, string search)
        {
            if (searchBy == "Titel")
            {
                return View(repo.GetAllShows().Where(x => x.Movie.Name == search || search == null).ToList());
            }
            else
            {
                return View(repo.GetAllShows().Where(x => x.StartTime.ToString() == search || search == null).ToList());
            }
        }

        //Geschreven door Gregor Hoogstad
        public ViewResult ManualSeatSelection()
        {
            char[] splitChar = new char[] { ',' };
            int orderId = (int)Session["OrderID"];
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

        //Geschreven door Frank Molengraaf
        public ActionResult iDealChooseBank(Order order)
        {
            return View(order);
        }

        public ActionResult iDealPayment()
        {
            //Gemaakt door: Frank Molengraaf
            //Gemaakt door: Frank van Dijke
            Order order = repo.GetOrderByID((int)Session["OrderID"]);
            string bank = Request["Bank"];
            switch (bank)
            {
                case "ABN":
                    ViewBag.Logo = "/Resources/ABN.png";
                    ViewBag.Naam = "ABN Amro";
                    break;

                case "Rabo":
                    ViewBag.Logo = "/Resources/rabo.png";
                    ViewBag.Naam = "Rabobank";
                    break;

                case "ING":
                    ViewBag.Logo = "/Resources/ING.png";
                    ViewBag.Naam = "ING Bank";
                    break;

                case "SNS":
                    ViewBag.Logo = "/Resources/SNS.png";
                    ViewBag.Naam = "SNS Bank";
                    break;

                case "ASN":
                    ViewBag.Logo = "/Resources/asn.png";
                    ViewBag.Naam = "ASN Bank";
                    break;

            }
            order = repo.GetOrderByID(order.OrderId);
            return View(order);
        }

        //Geschreven door Frank Molengraaf
        public ActionResult CreditCardPayment()
        {
            Order order = repo.GetOrderByID((int)Session["OrderID"]);
            return View(order);
        }

        public ActionResult ChoosePaymentMethod()
        {
            return View();
        }

        //Geschreven door Robert-Jan Kooijman
        public ActionResult Paid(Order order)
        {
            order = repo.GetOrderByID(order.OrderId);
            repo.UpdateOrder(order);
            print.CreateObject(order, (bool)Session["Language"]);
            if (Session["Language"].Equals(false))
            {
                @ViewBag.paid = "U heeft betaald.";
            }
            else
            {
                @ViewBag.paid = "You have paid.";
            }
            return View("Reserved", order);
        }

        //Geschreven door Robert-Jan Kooijman
        public ActionResult Reserved(Order order)
        {
            order = repo.GetOrderByID(order.OrderId);
            ViewBag.totalPrice = order.GetTotalPrice();
            if (!order.Paid)
            {
                if (Session["Language"].Equals(false))
                {
                    @ViewBag.paid = "U heeft nog niet betaald, u kunt uw reservering betalen en ophalen bij de touchscreens in onze vestiging.";
                }
                else
                {
                    @ViewBag.paid = "You have not paid yet, you can pay your order and pick it up at the touch screens in our establishment.";
                }
            }
            repo.UpdateOrder(order);
            return View(order);
        }

        //Geschreven door Frank Molengraaf
        public ActionResult changeLanguage()
        {
            List<Show> model = repo.GetAllShows().Where(x => x.StartTime > DateTime.Now).ToList();
            string language = Request["language"];

            if (language == "nl")
            {
                Session["Language"] = false;
            }
            else if (language == "en")
            {
                Session["Language"] = true;
            }
            setDays((bool)Session["Language"]);
            return View("Index", model);
        }

        //Geschreven door Ricardo Jobse
        private void setDays(bool language)
        {
            playDay = new List<SelectListItem>();

            if (Session == null || Session["Language"].Equals(false))
            {
                playDay.Add(new SelectListItem { Text = "Maandag", Value = "Monday" });
                playDay.Add(new SelectListItem { Text = "Dinsdag", Value = "Tuesday" });
                playDay.Add(new SelectListItem { Text = "Woensdag", Value = "Wednesday" });
                playDay.Add(new SelectListItem { Text = "Donderdag", Value = "Thursday" });
                playDay.Add(new SelectListItem { Text = "Vrijdag", Value = "Friday" });
                playDay.Add(new SelectListItem { Text = "Zaterdag", Value = "Saturday" });
                playDay.Add(new SelectListItem { Text = "Zondag", Value = "Sunday" });
                playDay.Add(new SelectListItem { Text = "", Value = null, Selected = true });
            }
            else
            {
                playDay.Add(new SelectListItem { Text = "Monday", Value = "Monday" });
                playDay.Add(new SelectListItem { Text = "Tuesday", Value = "Tuesday" });
                playDay.Add(new SelectListItem { Text = "Wednesday", Value = "Wednesday" });
                playDay.Add(new SelectListItem { Text = "Thursday", Value = "Thursday" });
                playDay.Add(new SelectListItem { Text = "Friday", Value = "Friday" });
                playDay.Add(new SelectListItem { Text = "Saturday", Value = "Saturday" });
                playDay.Add(new SelectListItem { Text = "Sunday", Value = "Sunday" });
                playDay.Add(new SelectListItem { Text = "", Value = null, Selected = true });
            }


            ViewBag.playDay = playDay;
        }

        //Geschreven Gregor Hoogstad
        private List<TicketSoort> getAvailableTickets(Show show)
        {
            List<TicketSoort> tickets = repo.GetTicketSoorten().ToList();
            List<TicketSoort> aangebodenTickets = new List<TicketSoort>();

            //Nieuw
            if (show.LadiesNight != null && show.LadiesNight.LadiesNightDay.DayOfYear == show.StartTime.DayOfYear)
            {
                aangebodenTickets.Add(tickets.First(r => r.TicketSoortID == 4));
            }
            else
            {
                aangebodenTickets.Add(tickets.First(r => r.TicketSoortID == 1));

                if (show.PopcornArrangement)
                {
                    aangebodenTickets.Add(tickets.First(r => r.TicketSoortID == 5));
                }
                if (!show.Holiday && (show.StartTime.DayOfWeek > DayOfWeek.Sunday && show.StartTime.DayOfWeek < DayOfWeek.Friday))
                {
                    aangebodenTickets.Add(tickets.First(r => r.TicketSoortID == 6));
                }
                if (show.StartTime.DayOfWeek > DayOfWeek.Sunday && show.StartTime.DayOfWeek < DayOfWeek.Friday)
                {
                    aangebodenTickets.Add(tickets.First(r => r.TicketSoortID == 2));
                }
                if (show.Movie.Genre.Equals("Kinderfilm") && show.Movie.Language.Equals("Nederlands") && show.StartTime.Hour < 18)
                {
                    aangebodenTickets.Add(tickets.First(r => r.TicketSoortID == 3));
                }
            }
            return aangebodenTickets;
        }
    }
}