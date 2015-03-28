using BioscoopSysteemWebsite.Domain.Entities;
using BioscoopSysteemWebsite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BioscoopSysteemAdmin.WebUI.Controllers {
    public class PlannerController : Controller {

        private IRepository repo;

        public PlannerController(IRepository repo) {
            this.repo = repo;
        }

        public ActionResult Overview(int userid) {
            List<Show> showList = repo.GetAllShows().OrderBy(s => s.StartTime).ToList();
            ViewBag.Userid = userid;
            return View(showList);
        }

        [HttpGet]
        public ActionResult VoorstellingToevoegen(int userid) {
            List<LadiesNight> ladiesNightList = repo.GetAllLadiesNights().Where(l => l.LadiesNightDay.DayOfYear > DateTime.Now.DayOfYear).ToList();
            ViewBag.Userid = userid;
            ViewBag.ladiesNightSelect = ladiesNightList;
            ViewBag.RoomSelect = RoomSelect();
            ViewBag.MovieSelect = MovieSelect();
            return View();
        }

        [HttpPost]
        public ActionResult VoorstellingToevoegen(Show show) {
            DateTime date = DateTime.Parse(Request["datum"]);
            DateTime newDate = date.Add(TimeSpan.Parse(Request["starttijd"]));
            show.Movie = repo.GetMovieById(show.MovieId);
            List<Room> roomList = repo.GetAllRooms().ToList();
            List<Show> showList = repo.GetAllShows().Where(s => s.Room == repo.GetRoomById(show.RoomId)).ToList();
            show.Room = repo.GetRoomById(show.RoomId);
            if (repo.GetLadiesNightByDate(newDate) == null) {
                show.LadiesNight = null;
            } else {
                show.LadiesNight = repo.GetLadiesNightByDate(newDate);
                show.LadiesNightid = show.LadiesNight.LadiesNightid;
            }

            foreach (Room r in roomList) {
                //Check  of de zaal wel goede type is
                if (show.Movie.Type == true && r.Type == false && r.RoomId == show.Room.RoomId) {
                    List<LadiesNight> ladiesNightList = repo.GetAllLadiesNights().Where(l => l.LadiesNightDay.DayOfYear > DateTime.Now.DayOfYear).ToList();
                    ViewBag.ladiesNightSelect = ladiesNightList;
                    ViewBag.RoomSelect = RoomSelect();
                    ViewBag.MovieSelect = MovieSelect();
                    ViewBag.RoomError = "De zaal ondersteunt geen 3D";
                    return View();
                } else {
                    show.Room = r;
                }
            }

            //Check of de film nog beschikbaar is
            if(newDate.Date < show.Movie.EndDate.Date) {
                foreach (Show x in showList) {
                    Double doubleDuration = Double.Parse(show.Movie.Duration.ToString());

                    //Check of de voorstelling niet overlapt met andere voorstellingen
                    if (newDate <= (x.StartTime.AddMinutes(Double.Parse(x.Movie.Duration.ToString()))) && newDate.AddMinutes(doubleDuration) >= x.StartTime) {
                        List<LadiesNight> ladiesNightList = repo.GetAllLadiesNights().Where(l => l.LadiesNightDay.DayOfYear > DateTime.Now.DayOfYear).ToList();
                        ViewBag.ladiesNightSelect = ladiesNightList;
                        ViewBag.RoomSelect = RoomSelect();
                        ViewBag.MovieSelect = MovieSelect();
                        ViewBag.TimeError = "Deze zaal is bezet op de gegeven starttijd.";
                        return View();
                    }
                }
                show.StartTime = newDate;
            } else {
                List<LadiesNight> ladiesNightList = repo.GetAllLadiesNights().Where(l => l.LadiesNightDay.DayOfYear > DateTime.Now.DayOfYear).ToList();
                ViewBag.ladiesNightSelect = ladiesNightList;
                ViewBag.RoomSelect = RoomSelect();
                ViewBag.MovieSelect = MovieSelect();
                ViewBag.DateError = "De film is op deze dag niet meer beschikbaar.";
            }

            repo.AddShow(show);

            return View("Index", "Home");
        }

        private List<SelectListItem> MovieSelect() {
            List<SelectListItem> movieSelect = new List<SelectListItem>();
            List<Movie> availableMovies = repo.GetAllMovies().Where(m => m.StartDate < DateTime.Now && m.EndDate > DateTime.Now).ToList();
            SelectListItem x = null;
            foreach (Movie movie in availableMovies) {
                for (int i = 0; i < availableMovies.Count(); i++) {
                    x = new SelectListItem() { Text = movie.Name, Value = movie.MovieId.ToString() };
                }
                movieSelect.Add(x);
            }
            return movieSelect;
        }

        private List<SelectListItem> RoomSelect() {
            List<SelectListItem> roomSelect = new List<SelectListItem>();
            List<Room> allRooms =  repo.GetAllRooms().ToList();
             SelectListItem x = null;
            foreach (Room room in allRooms) {
                for (int i = 0; i < allRooms.Count(); i++) {
                    x = new SelectListItem() { Text = room.RoomNumber.ToString(), Value = room.RoomNumber.ToString() };
                }
                roomSelect.Add(x);
            }
            return roomSelect;
        }
    }
}